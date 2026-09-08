using Application.Features.Cart.DTOs;
using Application.Features.Cart.Service;
using Application.Features.Orders.DTOs;
using Application.Features.Orders.Service;
using Domain.Entities;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Souqy.Tests
{
    /// <summary>
    /// Grading criterion: two simultaneous POST /api/v1/orders requests carrying the
    /// same IdempotencyKey must result in exactly ONE order row in the database.
    ///
    /// Uses a file-backed SQLite database (not EF InMemory) because only a real
    /// relational provider enforces the unique index on Orders.IdempotencyKey and
    /// allows two independent connections to race.
    /// </summary>
    public class OrderIdempotencyConcurrencyTests : IDisposable
    {
        private readonly string _dbPath = Path.Combine(Path.GetTempPath(), $"souqy_idem_{Guid.NewGuid():N}.db");

        private DbContextOptions<ApplicationDbContext> Options() =>
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite($"Data Source={_dbPath}")
                .Options;

        private ApplicationDbContext NewContext() => new(Options());

        private static OrderService NewOrderService(ApplicationDbContext ctx) =>
            new(ctx, NullLogger<OrderService>.Instance);

        [Fact]
        public async Task TwoSimultaneousOrders_SameIdempotencyKey_CreateExactlyOneOrder()
        {
            // Arrange -----------------------------------------------------------
            var productId = Guid.NewGuid();
            var variantId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            // Category seeded by ApplicationDbContext.OnModelCreating (HasData).
            var seededCategoryId = new Guid("11111111-1111-1111-1111-111111111111");
            const string sharedKey = "CHECKOUT-KEY-RACE-1";

            using (var setup = NewContext())
            {
                await setup.Database.EnsureCreatedAsync();

                setup.Users.Add(new User
                {
                    Id = userId,
                    FullName = "Race Tester",
                    Email = $"race_{userId:N}@example.com",
                    PasswordHash = "x",
                    Role = "Customer",
                    CreatedAt = DateTime.UtcNow
                });
                setup.Products.Add(new Product
                {
                    Id = productId,
                    Name = "Race Jacket",
                    Price = 100m,
                    CategoryId = seededCategoryId,
                    CreatedAt = DateTime.UtcNow
                });
                setup.ProductVariants.Add(new ProductVariant
                {
                    Id = variantId,
                    ProductId = productId,
                    Size = "M",
                    Color = "Black",
                    StockQuantity = 10
                });
                await setup.SaveChangesAsync();

                var cartService = new CartService(setup, NullLogger<CartService>.Instance);
                await cartService.AddToCartAsync(userId, new AddToCartDto
                {
                    ProductId = productId,
                    ProductVariantId = variantId,
                    Quantity = 2
                });
            }

            var dto = new CreateOrderDto
            {
                ShippingAddress = "1 Race Way",
                IdempotencyKey = sharedKey
            };

            // Act: fire both requests concurrently, each on its own DbContext ---
            async Task<(OrderDto? order, Exception? error)> Fire()
            {
                try
                {
                    await using var ctx = NewContext();
                    var order = await NewOrderService(ctx).CreateOrderAsync(userId, dto);
                    return (order, null);
                }
                catch (Exception ex)
                {
                    return (null, ex);
                }
            }

            var results = await Task.WhenAll(Task.Run(Fire), Task.Run(Fire));

            // Assert ----------------------------------------------------------
            var successes = results.Where(r => r.order != null).Select(r => r.order!).ToList();
            Assert.Empty(results.Where(r => r.error != null).Select(r => r.error!));
            Assert.Equal(2, successes.Count); // both calls return a DTO...
            Assert.Equal(successes[0].Id, successes[1].Id); // ...for the SAME order

            await using var verify = NewContext();
            var orderCount = await verify.Orders.CountAsync(o => o.IdempotencyKey == sharedKey);
            Assert.Equal(1, orderCount);

            // Stock deducted exactly once (10 - 2), not twice.
            var stock = (await verify.ProductVariants.FindAsync(variantId))!.StockQuantity;
            Assert.Equal(8, stock);
        }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (File.Exists(_dbPath)) File.Delete(_dbPath);
        }
    }
}
