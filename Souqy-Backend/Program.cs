using Infrastructure;
using Application;
using Souqy.Middleware;

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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply the CORS policy before authorization so preflight requests are handled.
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
