using Microsoft.Extensions.DependencyInjection;
using Application.Features.Products.Services;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<ProductService>();
            services.AddScoped<Application.Features.Categories.Services.CategoryService>();

            // Register Mapster mapping configuration (global)
            Application.Mapping.MapsterConfig.Register();

            // Add memory cache (used by product caching)
            services.AddMemoryCache();

            return services;
        }
    }
}
