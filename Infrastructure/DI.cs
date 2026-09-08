using System;
using Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Read connection string from provided configuration (appsettings/user-secrets/env).
            var conn = configuration.GetConnectionString("DefaultConnection")
                       ?? configuration["ConnectionStrings:DefaultConnection"]
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

            if (string.IsNullOrWhiteSpace(conn))
            {
                // No DB configured: register in-memory repositories so the API works without Postgres.
                services.AddScoped<IProductRepository, InMemoryProductRepository>();
                services.AddScoped<ICategoryRepository, InMemoryCategoryRepository>();
            }
            else
            {
                // Register DbContext and EF repositories when connection string is present.
                services.AddDbContext<ApplicationDbContext>((provider, options) =>
                {
                    options.UseNpgsql(conn);
                });

                services.AddScoped<IProductRepository, EfProductRepository>();
                services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            }

            return services;
        }
    }
}
