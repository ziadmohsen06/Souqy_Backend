using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products
{
    public class EfProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

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
            return await _db.Products.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync(ct);
        }
    }
}
