using System;
using Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Python AI service (embeddings + recommendations). Registered unconditionally;
            // it degrades gracefully when the Python service is unreachable.
            services.AddSingleton<IEmbeddingService, PythonAiService>();

            // Connection string from configuration (appsettings / user-secrets / env).
            var conn = configuration.GetConnectionString("DefaultConnection")
                       ?? configuration["ConnectionStrings:DefaultConnection"]
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(conn));

            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            return services;
        }
    }
}
