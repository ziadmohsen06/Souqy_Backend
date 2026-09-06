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

            // Register EF repository (scoped) and keep InMemory implementation present but not registered
            services.AddScoped<IProductRepository, EfProductRepository>();

            return services;
        }
    }
}
