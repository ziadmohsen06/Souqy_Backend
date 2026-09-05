using Souqy.Mapping;
using Souqy.Features.Products;
using Souqy.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// register product services
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<ProductService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Mapster mappings (no-op today but centralizes config)
MapsterConfig.Register();

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
