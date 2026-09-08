using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Infrastructure.Products
{
    public class EfProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient _httpClient = new HttpClient(); // to be able to call the python service to get recommendations for a product.

        public EfProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            await _db.Products.AddAsync(product, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _db.Products.FindAsync(new object[] { id }, ct);
            if (entity == null) return;
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Products.AsNoTracking().Include(p => p.Variants).ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Products.AsNoTracking().Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync(ct);
        }



        // This method calls the Python service by navigating to the endpoint containing the get_recommendations method (main.py file) to get recommendations for a product based on its ID. It then fetches the recommended products from the database and returns them.
        public async Task<IEnumerable<Product>> GetRecommendationsAsync(Guid productId, int count = 4, CancellationToken ct = default)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"http://localhost:8000/recommendations/{productId}?limit={count}", 
                    ct
                );
                
                if (!response.IsSuccessStatusCode) return Enumerable.Empty<Product>();

                var recommendations = await response.Content.ReadFromJsonAsync<List<PythonRecommendation>>(ct);
                if (recommendations == null || !recommendations.Any()) return Enumerable.Empty<Product>();

                var productIds = recommendations.Select(r => Guid.Parse(r.Id)).ToList();
                
                return await _db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .AsNoTracking()
                    .ToListAsync(ct);
            }
            catch
            {
                // If Python service is down, return empty list instead of crashing the app
                return Enumerable.Empty<Product>();
            }
        }
    }

    // Helper class to read the JSON response from Python
    internal class PythonRecommendation
    {
        public string Id { get; set; } = string.Empty;
    }
}
