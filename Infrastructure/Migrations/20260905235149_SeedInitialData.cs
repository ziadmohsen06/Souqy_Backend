using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Men's clothing and accessories", "Men" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Women's clothing and accessories", "Women" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Hats, belts, and more", "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Color", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "Size", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), "White", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A comfortable classic tee.", "", "Classic T-Shirt", 12.99m, "M", 100 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), "Blue", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Classic denim jeans.", "", "Denim Jeans", 49.50m, "32", 50 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), "Red", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Light summer dress.", "", "Summer Dress", 39.99m, "S", 40 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), "Black", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Comfortable heels.", "", "Heels", 59.99m, "38", 25 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("33333333-3333-3333-3333-333333333333"), "Navy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Stylish cap.", "", "Baseball Cap", 14.00m, "One Size", 200 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("33333333-3333-3333-3333-333333333333"), "Brown", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Genuine leather belt.", "", "Leather Belt", 25.00m, "L", 80 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
