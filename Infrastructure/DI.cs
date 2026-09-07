using Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;


namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // 1. Register the Python AI Service as a Singleton (no AddHttpClient needed)
            services.AddSingleton<IEmbeddingService, PythonAiService>();


            // register DbContext using connection string from configuration
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var conn = configuration.GetConnectionString("DefaultConnection")
                           ?? configuration["ConnectionStrings:DefaultConnection"];
                options.UseNpgsql(conn);
            });

            // // 2. HARDCODED CONNECTION STRING TO BYPASS ALL CONFIGURATION GHOSTS
            // services.AddDbContext<ApplicationDbContext>(options =>
            // {
            //     // ⚠️ REPLACE 'YOUR_REAL_PASSWORD' WITH YOUR ACTUAL POSTGRES PASSWORD ⚠️
            //     options.UseNpgsql("Host=localhost;Port=5433;Database=souqy;Username=postgres;Password=zisco2002;Include Error Detail=true");
            // });

            // Register EF repositories (scoped)
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            return services;
        }
    }
}
                