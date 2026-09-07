using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColorVariantsToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add new columns to ProductVariants
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductVariants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorImageUrl",
                table: "ProductVariants",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            // Remove old columns from Products (these moved to ProductVariants)
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            // Remove old constraint that won't apply anymore
            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_StockQuantity",
                table: "Products");

            // Add new check constraint for ProductVariants
            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductVariants_StockQuantity",
                table: "ProductVariants",
                sql: "\"StockQuantity\" >= 0");

            // Update unique index on ProductVariants
            migrationBuilder.DropIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants",
                columns: new[] { "ProductId", "Color" },
                unique: true);

            // Seed ProductVariants with four color variants for each product
            // Product 1: Classic T-Shirt
            var product1Id = new Guid("10000000-0000-0000-0000-000000000001");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), product1Id, "M", "White", "https://example.com/images/tshirt-white.jpg", 100 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), product1Id, "M", "Black", "https://example.com/images/tshirt-black.jpg", 80 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), product1Id, "M", "Brown", "https://example.com/images/tshirt-brown.jpg", 60 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), product1Id, "M", "Blue", "https://example.com/images/tshirt-blue.jpg", 90 }
                });

            // Product 2: Denim Jeans
            var product2Id = new Guid("10000000-0000-0000-0000-000000000002");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000005"), product2Id, "32", "White", "https://example.com/images/jeans-white.jpg", 30 },
                    { new Guid("20000000-0000-0000-0000-000000000006"), product2Id, "32", "Black", "https://example.com/images/jeans-black.jpg", 50 },
                    { new Guid("20000000-0000-0000-0000-000000000007"), product2Id, "32", "Brown", "https://example.com/images/jeans-brown.jpg", 20 },
                    { new Guid("20000000-0000-0000-0000-000000000008"), product2Id, "32", "Blue", "https://example.com/images/jeans-blue.jpg", 40 }
                });

            // Product 3: Summer Dress
            var product3Id = new Guid("10000000-0000-0000-0000-000000000003");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000009"), product3Id, "S", "White", "https://example.com/images/dress-white.jpg", 25 },
                    { new Guid("20000000-0000-0000-0000-000000000010"), product3Id, "S", "Black", "https://example.com/images/dress-black.jpg", 20 },
                    { new Guid("20000000-0000-0000-0000-000000000011"), product3Id, "S", "Brown", "https://example.com/images/dress-brown.jpg", 15 },
                    { new Guid("20000000-0000-0000-0000-000000000012"), product3Id, "S", "Blue", "https://example.com/images/dress-blue.jpg", 40 }
                });

            // Product 4: Heels
            var product4Id = new Guid("10000000-0000-0000-0000-000000000004");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000013"), product4Id, "38", "White", "https://example.com/images/heels-white.jpg", 15 },
                    { new Guid("20000000-0000-0000-0000-000000000014"), product4Id, "38", "Black", "https://example.com/images/heels-black.jpg", 25 },
                    { new Guid("20000000-0000-0000-0000-000000000015"), product4Id, "38", "Brown", "https://example.com/images/heels-brown.jpg", 10 },
                    { new Guid("20000000-0000-0000-0000-000000000016"), product4Id, "38", "Blue", "https://example.com/images/heels-blue.jpg", 18 }
                });

            // Product 5: Baseball Cap
            var product5Id = new Guid("10000000-0000-0000-0000-000000000005");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000017"), product5Id, "One Size", "White", "https://example.com/images/cap-white.jpg", 150 },
                    { new Guid("20000000-0000-0000-0000-000000000018"), product5Id, "One Size", "Black", "https://example.com/images/cap-black.jpg", 200 },
                    { new Guid("20000000-0000-0000-0000-000000000019"), product5Id, "One Size", "Brown", "https://example.com/images/cap-brown.jpg", 100 },
                    { new Guid("20000000-0000-0000-0000-000000000020"), product5Id, "One Size", "Blue", "https://example.com/images/cap-blue.jpg", 180 }
                });

            // Product 6: Leather Belt
            var product6Id = new Guid("10000000-0000-0000-0000-000000000006");
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "Size", "Color", "ColorImageUrl", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000021"), product6Id, "L", "White", "https://example.com/images/belt-white.jpg", 40 },
                    { new Guid("20000000-0000-0000-0000-000000000022"), product6Id, "L", "Black", "https://example.com/images/belt-black.jpg", 80 },
                    { new Guid("20000000-0000-0000-0000-000000000023"), product6Id, "L", "Brown", "https://example.com/images/belt-brown.jpg", 70 },
                    { new Guid("20000000-0000-0000-0000-000000000024"), product6Id, "L", "Blue", "https://example.com/images/belt-blue.jpg", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all seeded ProductVariants
            for (int i = 1; i <= 24; i++)
            {
                migrationBuilder.DeleteData(
                    table: "ProductVariants",
                    keyColumn: "Id",
                    keyValue: new Guid($"20000000-0000-0000-0000-{i:000000000000}"));
            }

            // Drop the unique index
            migrationBuilder.DropIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants");

            // Remove check constraint
            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductVariants_StockQuantity",
                table: "ProductVariants");

            // Remove columns from ProductVariants
            migrationBuilder.DropColumn(
                name: "ColorImageUrl",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductVariants");

            // Recreate old columns on Products
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Recreate old constraint
            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_StockQuantity",
                table: "Products",
                sql: "\"StockQuantity\" >= 0");

            // Recreate old unique index
            migrationBuilder.CreateIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants",
                columns: new[] { "ProductId", "Color" });
        }
    }
}
