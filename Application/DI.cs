using Microsoft.Extensions.DependencyInjection;
using Application.Features.Products.Services;
using Application.Features.Cart.Service;
using Application.Features.Orders.Service;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<ProductService>();
            services.AddScoped<Application.Features.Categories.Services.CategoryService>();
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();

            // Register Mapster mapping configuration (global)
            Application.Mapping.MapsterConfig.Register();

            // Add memory cache (used by product caching)
            services.AddMemoryCache();

            return services;
        }
    }
}
