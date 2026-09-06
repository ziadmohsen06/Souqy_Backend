using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public EfCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Categories.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        }
    }
}
