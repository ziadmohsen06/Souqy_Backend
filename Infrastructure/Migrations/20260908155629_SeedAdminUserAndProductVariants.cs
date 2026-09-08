using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserAndProductVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "ColorImageUrl", "ProductId", "Size", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000000101"), "White", null, new Guid("10000000-0000-0000-0000-000000000001"), "M", 60 },
                    { new Guid("b0000000-0000-0000-0000-000000000102"), "Black", null, new Guid("10000000-0000-0000-0000-000000000001"), "L", 40 },
                    { new Guid("b0000000-0000-0000-0000-000000000103"), "Navy", null, new Guid("10000000-0000-0000-0000-000000000001"), "S", 25 },
                    { new Guid("b0000000-0000-0000-0000-000000000201"), "Blue", null, new Guid("10000000-0000-0000-0000-000000000002"), "32", 50 },
                    { new Guid("b0000000-0000-0000-0000-000000000202"), "Black", null, new Guid("10000000-0000-0000-0000-000000000002"), "34", 20 },
                    { new Guid("b0000000-0000-0000-0000-000000000301"), "Red", null, new Guid("10000000-0000-0000-0000-000000000003"), "S", 25 },
                    { new Guid("b0000000-0000-0000-0000-000000000302"), "Blue", null, new Guid("10000000-0000-0000-0000-000000000003"), "M", 12 },
                    { new Guid("b0000000-0000-0000-0000-000000000303"), "Yellow", null, new Guid("10000000-0000-0000-0000-000000000003"), "S", 8 },
                    { new Guid("b0000000-0000-0000-0000-000000000401"), "Black", null, new Guid("10000000-0000-0000-0000-000000000004"), "38", 20 },
                    { new Guid("b0000000-0000-0000-0000-000000000402"), "Nude", null, new Guid("10000000-0000-0000-0000-000000000004"), "37", 10 },
                    { new Guid("b0000000-0000-0000-0000-000000000501"), "Navy", null, new Guid("10000000-0000-0000-0000-000000000005"), "One Size", 120 },
                    { new Guid("b0000000-0000-0000-0000-000000000502"), "Black", null, new Guid("10000000-0000-0000-0000-000000000005"), "One Size", 80 },
                    { new Guid("b0000000-0000-0000-0000-000000000503"), "Olive", null, new Guid("10000000-0000-0000-0000-000000000005"), "One Size", 40 },
                    { new Guid("b0000000-0000-0000-0000-000000000601"), "Brown", null, new Guid("10000000-0000-0000-0000-000000000006"), "M", 50 },
                    { new Guid("b0000000-0000-0000-0000-000000000602"), "Black", null, new Guid("10000000-0000-0000-0000-000000000006"), "L", 40 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@souqy.local", "Souqy Admin", "$2a$11$Fk9B2IKFSWmqiZ4/GS/BueBRgv2Qfmyoyvi38gSloHTim10ngWlU2", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000202"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000301"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000302"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000303"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000401"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000402"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000501"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000502"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000503"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000601"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000602"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));
        }
    }
}
