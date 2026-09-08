using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Souqy.Middleware;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(userSecretsId: "923bec48-b0c6-4f59-81cb-818b30197022");


// Add services to the container.
builder.Services.AddControllers();

// CORS: allow the frontend origin(s). Configure via appsettings or env var:
//   - appsettings.json: "Cors:AllowedOrigins": "https://prod.example.com;http://localhost:5173"
//   - or environment variable: CORS_ALLOWED_ORIGINS (semicolon-separated)
var corsOriginsConfig = builder.Configuration["Cors:AllowedOrigins"]
                        ?? builder.Configuration["CORS_ALLOWED_ORIGINS"]
                        ?? "http://localhost:5173";
var allowedOrigins = corsOriginsConfig.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure infrastructure & application services
Infrastructure.DI.ConfigureServices(builder.Services, builder.Configuration);
Application.DI.ConfigureServices(builder.Services);

// JWT Authentication.
// The app must always boot and serve public endpoints even with no Jwt config.
// If Jwt:Key is missing we fall back to a well-known insecure development key
// (the same one AuthService uses so locally-issued tokens still validate) and
// warn at startup. Auth-protected endpoints then fail per-request rather than
// crashing the whole process. Real environments MUST set Jwt:Key / Issuer /
// Audience via user secrets or environment variables.
const string DevFallbackJwtKey = "SuperSecretKeyForAuthPartSystemTesting12345!";
const string DevFallbackJwtIssuer = "AuthPartIssuer";
const string DevFallbackJwtAudience = "AuthPartAudience";

var jwtKeyConfigured = !string.IsNullOrWhiteSpace(builder.Configuration["Jwt:Key"]);
var jwtKey = jwtKeyConfigured ? builder.Configuration["Jwt:Key"]! : DevFallbackJwtKey;
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? DevFallbackJwtIssuer;
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? DevFallbackJwtAudience;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// Rate limiting (built into the ASP.NET Core shared framework).
// Policies are opt-in per endpoint via [EnableRateLimiting("...")]. Each partition
// is keyed by the authenticated user id when present, otherwise the client IP, so
// one abusive caller can't lock out everyone behind the same NAT for long.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Brute-force protection for credential endpoints.
    options.AddPolicy("auth", http => PartitionFactory(PartitionKey(http), permitLimit: 5));

    // Abuse protection for checkout.
    options.AddPolicy("checkout", http => PartitionFactory(PartitionKey(http), permitLimit: 10));

    options.OnRejected = async (context, token) =>
    {
        var retryAfter = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var ra)
            ? (int)ra.TotalSeconds
            : 60;
        context.HttpContext.Response.Headers.RetryAfter = retryAfter.ToString();
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            statusCode = StatusCodes.Status429TooManyRequests,
            message = "Too many requests. Please wait a moment and try again."
        }), token);
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (!jwtKeyConfigured)
{
    app.Services.GetRequiredService<ILoggerFactory>()
        .CreateLogger("Souqy.Startup")
        .LogWarning(
            "Jwt:Key is not configured - using an INSECURE development fallback key. " +
            "The app boots and public endpoints work, but JWT auth is not secure and " +
            "tokens from other environments will not validate. Set Jwt:Key (and " +
            "Jwt:Issuer / Jwt:Audience) via user secrets or environment variables.");
}

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply the CORS policy before authorization so preflight requests are handled.
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();

static string PartitionKey(HttpContext http) =>
    http.User.FindFirstValue(ClaimTypes.NameIdentifier)
    ?? http.Connection.RemoteIpAddress?.ToString()
    ?? "unknown";

static RateLimitPartition<string> PartitionFactory(string key, int permitLimit) =>
    RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
    {
        PermitLimit = permitLimit,
        Window = TimeSpan.FromMinutes(1),
        QueueLimit = 0,
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
    });
