using Microsoft.Extensions.DependencyInjection;
using Application.Features.Auth.Service;
using Application.Features.Cart.Service;
using Application.Features.Orders.Service;
using Application.Features.Products.Services;
using Application.Features.Categories.Services;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<AuthService>();
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductVariantService>();
            services.AddScoped<CategoryService>();

            // Register Mapster mapping configuration (global)
            Application.Mapping.MapsterConfig.Register();

            // Add memory cache (used by product caching)
            services.AddMemoryCache();

            return services;
        }
    }
}
