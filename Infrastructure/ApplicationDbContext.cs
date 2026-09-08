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
            // IdempotencyRecord is defined in Domain and reachable via the User.IdempotencyRecords
            // navigation, so EF discovers it by convention — but nothing configures or uses it yet
            // (OrderService relies on Order.IdempotencyKey instead). Exclude it from the model so it
            // doesn't add an unexpected table to the shared database. Remove this Ignore when the
            // idempotency-record feature is actually built and configured.
            modelBuilder.Ignore<Domain.Entities.IdempotencyRecord>();

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.ToTable("ProductVariants", t =>
                    t.HasCheckConstraint("CK_ProductVariants_StockQuantity", "\"StockQuantity\" >= 0"));
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

                entity.HasIndex(v => new { v.ProductId, v.Color })
                    .IsUnique()
                    .HasDatabaseName("UQ_ProductVariants_Product_Color");
            });
            // USERS
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users", t =>
                    t.HasCheckConstraint("CK_Users_Role", "\"Role\" IN ('Customer','Admin')"));
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(u => u.FullName).HasMaxLength(100).IsRequired().HasColumnName("FullName");
                b.Property(u => u.Email).HasMaxLength(150).IsRequired().HasColumnName("Email");
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired().HasColumnName("PasswordHash");
                b.Property(u => u.Role).HasMaxLength(20).IsRequired().HasDefaultValue("Customer").HasColumnName("Role");
                b.Property(u => u.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");
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
                b.ToTable("Products", t =>
                    t.HasCheckConstraint("CK_Products_Price", "\"Price\" >= 0"));
                b.HasKey(p => p.Id);
                b.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(p => p.Name).HasMaxLength(150).IsRequired().HasColumnName("Name");
                b.Property(p => p.Description).HasColumnType("text").HasColumnName("Description");
                b.Property(p => p.Price).HasColumnType("numeric(10,2)").IsRequired().HasColumnName("Price");
                b.Property(p => p.Color).HasMaxLength(50).HasColumnName("Color");
                b.Property(p => p.ImageUrl).HasMaxLength(500).HasColumnName("ImageUrl");
                b.Property(p => p.CategoryId).IsRequired().HasColumnName("CategoryId");
                b.Property(p => p.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");
                b.Property(p => p.Embedding)
                 .HasColumnType("text")
                 .HasColumnName("Embedding");

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
                b.ToTable("CartItems", t =>
                    t.HasCheckConstraint("CK_CartItems_Quantity", "\"Quantity\" > 0"));
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
                b.ToTable("Orders", t =>
                {
                    t.HasCheckConstraint("CK_Orders_Status", "\"Status\" IN ('Pending','Paid','Failed','Cancelled')");
                    t.HasCheckConstraint("CK_Orders_TotalAmount", "\"TotalAmount\" >= 0");
                });
                b.HasKey(o => o.Id);
                b.Property(o => o.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(o => o.UserId).IsRequired().HasColumnName("UserId");
                b.Property(o => o.IdempotencyKey).HasMaxLength(100).IsRequired().HasColumnName("IdempotencyKey");
                b.HasIndex(o => o.IdempotencyKey).IsUnique();
                b.Property(o => o.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Pending").HasColumnName("Status");
                b.Property(o => o.TotalAmount).HasColumnType("numeric(10,2)").IsRequired().HasDefaultValue(0).HasColumnName("TotalAmount");
                b.Property(o => o.ShippingAddress).HasMaxLength(500).IsRequired().HasColumnName("ShippingAddress");
                b.Property(o => o.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("CreatedAt");

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
                b.ToTable("OrderItems", t =>
                {
                    t.HasCheckConstraint("CK_OrderItems_UnitPrice", "\"UnitPrice\" >= 0");
                    t.HasCheckConstraint("CK_OrderItems_Quantity", "\"Quantity\" > 0");
                });
                b.HasKey(oi => oi.Id);
                b.Property(oi => oi.Id).HasDefaultValueSql("gen_random_uuid()");
                b.Property(oi => oi.OrderId).IsRequired().HasColumnName("OrderId");
                b.Property(oi => oi.ProductId).HasColumnName("ProductId");
                b.Property(oi => oi.ProductName).HasMaxLength(150).IsRequired().HasColumnName("ProductName");
                b.Property(oi => oi.Color).HasMaxLength(50).HasColumnName("Color");
                b.Property(oi => oi.UnitPrice).HasColumnType("numeric(10,2)").IsRequired().HasColumnName("UnitPrice");
                b.Property(oi => oi.Quantity).IsRequired().HasColumnName("Quantity");

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

            // Real product imagery (free Unsplash stock photos, sized for the grid).
            static string Img(string id) => $"https://images.unsplash.com/{id}?w=900&auto=format&fit=crop&q=80";
            const string IMG_TEE = "photo-1521572163474-6864f9cf17ab";
            const string IMG_SHIRT = "photo-1602810318383-e386cc2a3ccf";
            const string IMG_SWEATER = "photo-1576871337622-98d48d1cf531";
            const string IMG_HOODIE = "photo-1556905055-8f358a7a47b2";
            const string IMG_JEANS = "photo-1542272604-787c3835535d";
            const string IMG_CHINO = "photo-1624378439575-d8705ad7ae80";
            const string IMG_JACKET = "photo-1576995853123-5a10305d93c0";
            const string IMG_BLAZER = "photo-1591047139829-d91aecb6caea";
            const string IMG_DRESS = "photo-1539109136881-3be0616acf4b";
            const string IMG_HEELS = "photo-1543163521-1bf539c55dd2";
            const string IMG_CAP = "photo-1588850561407-ed78c282e89b";
            const string IMG_BELT = "photo-1624222247344-550fb60583dc";
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
                    ImageUrl = Img(IMG_TEE),
                    CategoryId = cat1,
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000002"),
                    Name = "Denim Jeans",
                    Description = "Classic denim jeans.",
                    Price = 49.50m,
                    Color = "Blue",
                    ImageUrl = Img(IMG_JEANS),
                    CategoryId = cat1,
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000003"),
                    Name = "Summer Dress",
                    Description = "Light summer dress.",
                    Price = 39.99m,
                    Color = "Red",
                    ImageUrl = Img(IMG_DRESS),
                    CategoryId = cat2,
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000004"),
                    Name = "Heels",
                    Description = "Comfortable heels.",
                    Price = 59.99m,
                    Color = "Black",
                    ImageUrl = Img(IMG_HEELS),
                    CategoryId = cat2,
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000005"),
                    Name = "Baseball Cap",
                    Description = "Stylish cap.",
                    Price = 14.00m,
                    Color = "Navy",
                    ImageUrl = Img(IMG_CAP),
                    CategoryId = cat3,
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000006"),
                    Name = "Leather Belt",
                    Description = "Genuine leather belt.",
                    Price = 25.00m,
                    Color = "Brown",
                    ImageUrl = Img(IMG_BELT),
                    CategoryId = cat3,
                    CreatedAt = seedDate
                },

                // --- Added for the demo: more depth per category ------------------
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000021"),
                    Name = "Merino Wool Sweater",
                    Description = "Mid-weight extra-fine Merino knit with ribbed trims — warm, breathable and not itchy.",
                    Price = 79.00m,
                    Color = "Oatmeal",
                    ImageUrl = Img(IMG_SWEATER),
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2025, 9, 5, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000022"),
                    Name = "Oxford Button-Down Shirt",
                    Description = "Garment-washed Oxford cotton with a soft roll collar. Wears equally well tucked or open over a tee.",
                    Price = 45.00m,
                    Color = "White",
                    ImageUrl = Img(IMG_SHIRT),
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2025, 10, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000023"),
                    Name = "Slim Chino Trousers",
                    Description = "Stretch-cotton twill chinos with a clean tapered leg, reinforced seams and deep front pockets.",
                    Price = 58.00m,
                    Color = "Khaki",
                    ImageUrl = Img(IMG_CHINO),
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2025, 7, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000024"),
                    Name = "Bomber Jacket",
                    Description = "Lightweight water-repellent bomber with ribbed cuffs and a matte zip. Layers over knitwear all season.",
                    Price = 135.00m,
                    Color = "Black",
                    ImageUrl = Img(IMG_JACKET),
                    CategoryId = cat1,
                    CreatedAt = new DateTime(2026, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000025"),
                    Name = "Pleated Midi Skirt",
                    Description = "Fluid accordion-pleated midi with an elastic-back waistband for all-day comfort.",
                    Price = 62.00m,
                    Color = "Blush",
                    ImageUrl = Img(IMG_DRESS),
                    CategoryId = cat2,
                    CreatedAt = new DateTime(2025, 8, 22, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000026"),
                    Name = "Tailored Wool Blazer",
                    Description = "Half-canvassed single-breasted blazer in Italian wool with natural shoulders and working cuffs.",
                    Price = 168.00m,
                    Color = "Camel",
                    ImageUrl = Img(IMG_BLAZER),
                    CategoryId = cat2,
                    CreatedAt = new DateTime(2025, 11, 3, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000027"),
                    Name = "Silk Slip Dress",
                    Description = "Bias-cut sandwashed silk slip with adjustable straps and a subtle cowl neck.",
                    Price = 115.00m,
                    Color = "Champagne",
                    ImageUrl = Img(IMG_DRESS),
                    CategoryId = cat2,
                    CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000028"),
                    Name = "Ribbed Wool Beanie",
                    Description = "Chunky rib-knit lambswool beanie with a fold-over cuff. One size, generous fit.",
                    Price = 22.00m,
                    Color = "Grey",
                    ImageUrl = Img(IMG_CAP),
                    CategoryId = cat3,
                    CreatedAt = new DateTime(2025, 10, 20, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000029"),
                    Name = "Canvas Web Belt",
                    Description = "Cotton-webbing belt with a brushed-metal box buckle and leather keeper. Trim to fit.",
                    Price = 18.00m,
                    Color = "Khaki",
                    ImageUrl = Img(IMG_BELT),
                    CategoryId = cat3,
                    CreatedAt = new DateTime(2025, 6, 14, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seeded accounts. Passwords are BCrypt (work factor 11):
            //   admin@souqy.local / Admin123!      (Admin  — product & variant management)
            //   nour@souqy.local  / Customer123!   (Customer — demo shopper)
            //   omar@souqy.local  / Customer123!   (Customer — demo shopper)
            // Log in via POST /api/v1/auth/login.
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Souqy Admin",
                    Email = "admin@souqy.local",
                    PasswordHash = "$2a$11$Fk9B2IKFSWmqiZ4/GS/BueBRgv2Qfmyoyvi38gSloHTim10ngWlU2",
                    Role = "Admin",
                    CreatedAt = seedDate
                },
                new User
                {
                    Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                    FullName = "Nour Hassan",
                    Email = "nour@souqy.local",
                    PasswordHash = "$2a$11$FFNhXAkvCy20R2rbAa1mFOetTfdMbfKbb9D0ZVfvAoDRh67qBzxuC",
                    Role = "Customer",
                    CreatedAt = seedDate
                },
                new User
                {
                    Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                    FullName = "Omar Khaled",
                    Email = "omar@souqy.local",
                    PasswordHash = "$2a$11$FFNhXAkvCy20R2rbAa1mFOetTfdMbfKbb9D0ZVfvAoDRh67qBzxuC",
                    Role = "Customer",
                    CreatedAt = seedDate
                });

            // Purchasable inventory: at least one ProductVariant per seeded product.
            // The unique index UQ_ProductVariants_Product_Color means each colour of a
            // product is a single row (its own size + stock).
            modelBuilder.Entity<ProductVariant>().HasData(
                // Classic T-Shirt
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000101"), ProductId = new Guid("10000000-0000-0000-0000-000000000001"), Color = "White", Size = "M", StockQuantity = 60, ColorImageUrl = Img(IMG_TEE) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000102"), ProductId = new Guid("10000000-0000-0000-0000-000000000001"), Color = "Black", Size = "L", StockQuantity = 40, ColorImageUrl = Img(IMG_SHIRT) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000103"), ProductId = new Guid("10000000-0000-0000-0000-000000000001"), Color = "Navy", Size = "S", StockQuantity = 25, ColorImageUrl = Img(IMG_SWEATER) },
                // Denim Jeans
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000201"), ProductId = new Guid("10000000-0000-0000-0000-000000000002"), Color = "Blue", Size = "32", StockQuantity = 50, ColorImageUrl = Img(IMG_JEANS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000202"), ProductId = new Guid("10000000-0000-0000-0000-000000000002"), Color = "Black", Size = "34", StockQuantity = 20, ColorImageUrl = Img(IMG_JEANS) },
                // Summer Dress
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000301"), ProductId = new Guid("10000000-0000-0000-0000-000000000003"), Color = "Red", Size = "S", StockQuantity = 25, ColorImageUrl = Img(IMG_DRESS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000302"), ProductId = new Guid("10000000-0000-0000-0000-000000000003"), Color = "Blue", Size = "M", StockQuantity = 12, ColorImageUrl = Img(IMG_DRESS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000303"), ProductId = new Guid("10000000-0000-0000-0000-000000000003"), Color = "Yellow", Size = "S", StockQuantity = 8, ColorImageUrl = Img(IMG_DRESS) },
                // Heels
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000401"), ProductId = new Guid("10000000-0000-0000-0000-000000000004"), Color = "Black", Size = "38", StockQuantity = 20, ColorImageUrl = Img(IMG_HEELS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000402"), ProductId = new Guid("10000000-0000-0000-0000-000000000004"), Color = "Nude", Size = "37", StockQuantity = 10, ColorImageUrl = Img(IMG_HEELS) },
                // Baseball Cap
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000501"), ProductId = new Guid("10000000-0000-0000-0000-000000000005"), Color = "Navy", Size = "One Size", StockQuantity = 120, ColorImageUrl = Img(IMG_CAP) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000502"), ProductId = new Guid("10000000-0000-0000-0000-000000000005"), Color = "Black", Size = "One Size", StockQuantity = 80, ColorImageUrl = Img(IMG_CAP) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000503"), ProductId = new Guid("10000000-0000-0000-0000-000000000005"), Color = "Olive", Size = "One Size", StockQuantity = 40, ColorImageUrl = Img(IMG_CAP) },
                // Leather Belt
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000601"), ProductId = new Guid("10000000-0000-0000-0000-000000000006"), Color = "Brown", Size = "M", StockQuantity = 50, ColorImageUrl = Img(IMG_BELT) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000000602"), ProductId = new Guid("10000000-0000-0000-0000-000000000006"), Color = "Black", Size = "L", StockQuantity = 40, ColorImageUrl = Img(IMG_BELT) },

                // --- Variants for the demo products -----------------------------
                // Merino Wool Sweater
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002101"), ProductId = new Guid("20000000-0000-0000-0000-000000000021"), Color = "Oatmeal", Size = "M", StockQuantity = 30, ColorImageUrl = Img(IMG_SWEATER) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002102"), ProductId = new Guid("20000000-0000-0000-0000-000000000021"), Color = "Charcoal", Size = "L", StockQuantity = 18, ColorImageUrl = Img(IMG_HOODIE) },
                // Oxford Button-Down Shirt
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002201"), ProductId = new Guid("20000000-0000-0000-0000-000000000022"), Color = "White", Size = "M", StockQuantity = 40, ColorImageUrl = Img(IMG_SHIRT) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002202"), ProductId = new Guid("20000000-0000-0000-0000-000000000022"), Color = "Sky Blue", Size = "L", StockQuantity = 25, ColorImageUrl = Img(IMG_TEE) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002203"), ProductId = new Guid("20000000-0000-0000-0000-000000000022"), Color = "Pink", Size = "S", StockQuantity = 12, ColorImageUrl = Img(IMG_SWEATER) },
                // Slim Chino Trousers
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002301"), ProductId = new Guid("20000000-0000-0000-0000-000000000023"), Color = "Khaki", Size = "32", StockQuantity = 22, ColorImageUrl = Img(IMG_CHINO) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002302"), ProductId = new Guid("20000000-0000-0000-0000-000000000023"), Color = "Navy", Size = "34", StockQuantity = 18, ColorImageUrl = Img(IMG_JEANS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002303"), ProductId = new Guid("20000000-0000-0000-0000-000000000023"), Color = "Olive", Size = "30", StockQuantity = 10, ColorImageUrl = Img(IMG_CHINO) },
                // Bomber Jacket
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002401"), ProductId = new Guid("20000000-0000-0000-0000-000000000024"), Color = "Black", Size = "M", StockQuantity = 12, ColorImageUrl = Img(IMG_JACKET) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002402"), ProductId = new Guid("20000000-0000-0000-0000-000000000024"), Color = "Olive", Size = "L", StockQuantity = 9, ColorImageUrl = Img(IMG_HOODIE) },
                // Pleated Midi Skirt
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002501"), ProductId = new Guid("20000000-0000-0000-0000-000000000025"), Color = "Blush", Size = "S", StockQuantity = 15, ColorImageUrl = Img(IMG_DRESS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002502"), ProductId = new Guid("20000000-0000-0000-0000-000000000025"), Color = "Black", Size = "M", StockQuantity = 15, ColorImageUrl = Img(IMG_BLAZER) },
                // Tailored Wool Blazer
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002601"), ProductId = new Guid("20000000-0000-0000-0000-000000000026"), Color = "Camel", Size = "M", StockQuantity = 8, ColorImageUrl = Img(IMG_BLAZER) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002602"), ProductId = new Guid("20000000-0000-0000-0000-000000000026"), Color = "Charcoal", Size = "S", StockQuantity = 6, ColorImageUrl = Img(IMG_JACKET) },
                // Silk Slip Dress
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002701"), ProductId = new Guid("20000000-0000-0000-0000-000000000027"), Color = "Champagne", Size = "S", StockQuantity = 10, ColorImageUrl = Img(IMG_DRESS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002702"), ProductId = new Guid("20000000-0000-0000-0000-000000000027"), Color = "Emerald", Size = "M", StockQuantity = 8, ColorImageUrl = Img(IMG_DRESS) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002703"), ProductId = new Guid("20000000-0000-0000-0000-000000000027"), Color = "Black", Size = "L", StockQuantity = 6, ColorImageUrl = Img(IMG_HEELS) },
                // Ribbed Wool Beanie
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002801"), ProductId = new Guid("20000000-0000-0000-0000-000000000028"), Color = "Grey", Size = "One Size", StockQuantity = 60, ColorImageUrl = Img(IMG_CAP) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002802"), ProductId = new Guid("20000000-0000-0000-0000-000000000028"), Color = "Black", Size = "One Size", StockQuantity = 45, ColorImageUrl = Img(IMG_CAP) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002803"), ProductId = new Guid("20000000-0000-0000-0000-000000000028"), Color = "Mustard", Size = "One Size", StockQuantity = 20, ColorImageUrl = Img(IMG_CAP) },
                // Canvas Web Belt
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002901"), ProductId = new Guid("20000000-0000-0000-0000-000000000029"), Color = "Khaki", Size = "M", StockQuantity = 40, ColorImageUrl = Img(IMG_BELT) },
                new ProductVariant { Id = new Guid("b0000000-0000-0000-0000-000000002902"), ProductId = new Guid("20000000-0000-0000-0000-000000000029"), Color = "Navy", Size = "L", StockQuantity = 30, ColorImageUrl = Img(IMG_BELT) }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
