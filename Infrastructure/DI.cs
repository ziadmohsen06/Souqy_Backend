using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Products;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register infrastructure repositories and data access
            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            return services;
        }
    }
}
