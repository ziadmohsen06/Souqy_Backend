using Infrastructure;
using Application;
using Souqy.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(userSecretsId: "923bec48-b0c6-4f59-81cb-818b30197022");


// Add services to the container.
builder.Services.AddControllers();

// Configure infrastructure & application services
Infrastructure.DI.ConfigureServices(builder.Services);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
