using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.ToTable("ProductVariants");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).HasDefaultValueSql("gen_random_uuid()");

                entity.Property(v => v.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Size");

                entity.Property(v => v.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Color");

                entity.Property(v => v.ColorImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ColorImageUrl");

                entity.Property(v => v.StockQuantity)
                    .IsRequired()
                    .HasColumnName("StockQuantity");

                entity.Property(v => v.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductId");

                entity.HasOne(v => v.Product)
                    .WithMany(p => p.Variants)
                    .HasForeignKey(v => v.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductVariants_Products");

                entity.HasCheckConstraint("CK_ProductVariants_StockQuantity", "\"StockQuantity\" >= 0");
                entity.HasIndex(v => new { v.ProductId, v.Color })
                    .IsUnique()
                    .HasDatabaseName("UQ_ProductVariants_Product_Color");
            });
            // USERS
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(u => u.FullName).HasMaxLength(100).IsRequired().HasColumnName("FullName");
                b.Property(u => u.Email).HasMaxLength(150).IsRequired().HasColumnName("Email");
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired().HasColumnName("PasswordHash");
                b.Property(u => u.Role).HasMaxLength(20).IsRequired().HasDefaultValue("Customer").HasColumnName("Role");
                b.Property(u => u.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");
                b.HasCheckConstraint("CK_Users_Role", "\"Role\" IN ('Customer','Admin')");
            });

            // CATEGORIES
            modelBuilder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(c => c.Name).HasMaxLength(100).IsRequired().HasColumnName("Name");
                b.HasIndex(c => c.Name).IsUnique();
                b.Property(c => c.Description).HasMaxLength(500).HasColumnName("Description");
            });

            // PRODUCTS
            modelBuilder.Entity<Product>(b =>
            {
                b.ToTable("Products");
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(p => p.Name).HasMaxLength(150).IsRequired().HasColumnName("Name");
                b.Property(p => p.Description).HasColumnType("text").HasColumnName("Description");
                b.Property(p => p.Price).HasColumnType("numeric(10,2)").IsRequired().HasColumnName("Price");
                b.Property(p => p.Color).HasMaxLength(50).HasColumnName("Color");
                b.Property(p => p.ImageUrl).HasMaxLength(500).HasColumnName("ImageUrl");
                b.Property(p => p.CategoryId).IsRequired().HasColumnName("CategoryId");
                b.Property(p => p.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");

                b.HasCheckConstraint("CK_Products_Price", "\"Price\" >= 0");

                b.HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_Products_Categories");

                b.HasIndex(p => p.CategoryId).HasDatabaseName("IX_Products_CategoryId");
            });

            // CARTS
            modelBuilder.Entity<Cart>(b =>
            {
                b.ToTable("Carts");
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(c => c.UserId).IsRequired().HasColumnName("UserId");
                b.Property(c => c.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");
                b.Property(c => c.UpdatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("UpdatedAt");

                b.HasIndex(c => c.UserId).IsUnique();
                b.HasOne(c => c.User)
                 .WithOne(u => u.Cart)
                 .HasForeignKey<Cart>(c => c.UserId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_Carts_Users");
            });

            // CART ITEMS
            modelBuilder.Entity<CartItem>(b =>
            {
                b.ToTable("CartItems");
                b.HasKey(ci => ci.Id);
                b.Property(ci => ci.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(ci => ci.CartId).IsRequired().HasColumnName("CartId");
                b.Property(ci => ci.ProductId).IsRequired().HasColumnName("ProductId");
                b.Property(ci => ci.ProductVariantId).IsRequired().HasColumnName("ProductVariantId");
                b.Property(ci => ci.ProductName).IsRequired().HasMaxLength(150).HasColumnName("ProductName");
                b.Property(ci => ci.Color).IsRequired().HasMaxLength(50).HasColumnName("Color");
                b.Property(ci => ci.ColorImageUrl).HasMaxLength(500).HasColumnName("ColorImageUrl");
                b.Property(ci => ci.UnitPrice).HasColumnType("numeric(10,2)").IsRequired().HasColumnName("UnitPrice");
                b.Property(ci => ci.Quantity).IsRequired().HasColumnName("Quantity");

                b.HasCheckConstraint("CK_CartItems_Quantity", "\"Quantity\" > 0");

                b.HasOne(ci => ci.Cart)
                 .WithMany(c => c.Items)
                 .HasForeignKey(ci => ci.CartId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_CartItems_Carts");

                b.HasOne(ci => ci.Product)
                 .WithMany(p => p.CartItems)
                 .HasForeignKey(ci => ci.ProductId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_CartItems_Products");

                b.HasOne(ci => ci.ProductVariant)
                 .WithMany()
                 .HasForeignKey(ci => ci.ProductVariantId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_CartItems_ProductVariants");

                b.HasIndex(ci => new { ci.CartId, ci.ProductVariantId }).IsUnique().HasDatabaseName("UQ_CartItems_Cart_ProductVariant");
                b.HasIndex(ci => ci.ProductId).HasDatabaseName("IX_CartItems_ProductId");
            });

            // ORDERS
            modelBuilder.Entity<Order>(b =>
            {
                b.ToTable("Orders");
                b.HasKey(o => o.Id);
                b.Property(o => o.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(o => o.UserId).IsRequired().HasColumnName("UserId");
                b.Property(o => o.IdempotencyKey).HasMaxLength(100).IsRequired().HasColumnName("IdempotencyKey");
                b.HasIndex(o => o.IdempotencyKey).IsUnique();
                b.Property(o => o.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Pending").HasColumnName("Status");
                b.Property(o => o.TotalAmount).HasColumnType("numeric(10,2)").IsRequired().HasDefaultValue(0).HasColumnName("TotalAmount");
                b.Property(o => o.ShippingAddress).HasMaxLength(500).IsRequired().HasColumnName("ShippingAddress");
                b.Property(o => o.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");

                b.HasCheckConstraint("CK_Orders_Status", "\"Status\" IN ('Pending','Paid','Failed','Cancelled')");
                b.HasCheckConstraint("CK_Orders_TotalAmount", "\"TotalAmount\" >= 0");

                b.HasOne(o => o.User)
                 .WithMany(u => u.Orders)
                 .HasForeignKey(o => o.UserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_Orders_Users");

                b.HasIndex(o => o.UserId).HasDatabaseName("IX_Orders_UserId");
                b.HasIndex(o => o.Status).HasDatabaseName("IX_Orders_Status");
            });

            // ORDER ITEMS
            modelBuilder.Entity<OrderItem>(b =>
            {
                b.ToTable("OrderItems");
                b.HasKey(oi => oi.Id);
                b.Property(oi => oi.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(oi => oi.OrderId).IsRequired().HasColumnName("OrderId");
                b.Property(oi => oi.ProductId).HasColumnName("ProductId");
                b.Property(oi => oi.ProductName).HasMaxLength(150).IsRequired().HasColumnName("ProductName");
                b.Property(oi => oi.Color).HasMaxLength(50).HasColumnName("Color");
                b.Property(oi => oi.UnitPrice).HasColumnType("numeric(10,2)").IsRequired().HasColumnName("UnitPrice");
                b.Property(oi => oi.Quantity).IsRequired().HasColumnName("Quantity");

                b.HasCheckConstraint("CK_OrderItems_UnitPrice", "\"UnitPrice\" >= 0");
                b.HasCheckConstraint("CK_OrderItems_Quantity", "\"Quantity\" > 0");

                b.HasOne(oi => oi.Order)
                 .WithMany(o => o.Items)
                 .HasForeignKey(oi => oi.OrderId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_OrderItems_Orders");

                b.HasOne(oi => oi.Product)
                 .WithMany(p => p.OrderItems)
                 .HasForeignKey(oi => oi.ProductId)
                 .OnDelete(DeleteBehavior.SetNull)
                 .HasConstraintName("FK_OrderItems_Products");

                b.HasIndex(oi => oi.OrderId).HasDatabaseName("IX_OrderItems_OrderId");
                b.HasIndex(oi => oi.ProductId).HasDatabaseName("IX_OrderItems_ProductId");
            });

            // SEED DATA
            // Static GUIDs to match requirement for HasData
            var cat1 = new Guid("11111111-1111-1111-1111-111111111111");
            var cat2 = new Guid("22222222-2222-2222-2222-222222222222");
            var cat3 = new Guid("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = cat1, Name = "Men", Description = "Men's clothing and accessories" },
                new Category { Id = cat2, Name = "Women", Description = "Women's clothing and accessories" },
                new Category { Id = cat3, Name = "Accessories", Description = "Hats, belts, and more" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000001"),
                    Name = "Classic T-Shirt",
                    Description = "A comfortable classic tee.",
                    Price = 12.99m,
                    Color = "White",
                    ImageUrl = "",
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000002"),
                    Name = "Denim Jeans",
                    Description = "Classic denim jeans.",
                    Price = 49.50m,
                    Color = "Blue",
                    ImageUrl = "",
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000003"),
                    Name = "Summer Dress",
                    Description = "Light summer dress.",
                    Price = 39.99m,
                    Color = "Red",
                    ImageUrl = "",
                    CategoryId = cat2,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000004"),
                    Name = "Heels",
                    Description = "Comfortable heels.",
                    Price = 59.99m,
                    Color = "Black",
                    ImageUrl = "",
                    CategoryId = cat2,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000005"),
                    Name = "Baseball Cap",
                    Description = "Stylish cap.",
                    Price = 14.00m,
                    Color = "Navy",
                    ImageUrl = "",
                    CategoryId = cat3,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000006"),
                    Name = "Leather Belt",
                    Description = "Genuine leather belt.",
                    Price = 25.00m,
                    Color = "Brown",
                    ImageUrl = "",
                    CategoryId = cat3,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
