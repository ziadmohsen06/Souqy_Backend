using Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // register DbContext using connection string from configuration
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var conn = configuration.GetConnectionString("DefaultConnection")
                           ?? configuration["ConnectionStrings:DefaultConnection"];
                options.UseNpgsql(conn);
            });

            // Register EF repositories (scoped)
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            return services;
        }
    }
}
