using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DemoSeedDataAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000103"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000202"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000301"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000302"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000303"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000401"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000402"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000501"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000502"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000503"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000601"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000602"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Color", "CreatedAt", "Description", "Embedding", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000021"), new Guid("11111111-1111-1111-1111-111111111111"), "Oatmeal", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Mid-weight extra-fine Merino knit with ribbed trims — warm, breathable and not itchy.", null, "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", "Merino Wool Sweater", 79.00m },
                    { new Guid("20000000-0000-0000-0000-000000000022"), new Guid("11111111-1111-1111-1111-111111111111"), "White", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Garment-washed Oxford cotton with a soft roll collar. Wears equally well tucked or open over a tee.", null, "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", "Oxford Button-Down Shirt", 45.00m },
                    { new Guid("20000000-0000-0000-0000-000000000023"), new Guid("11111111-1111-1111-1111-111111111111"), "Khaki", new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Stretch-cotton twill chinos with a clean tapered leg, reinforced seams and deep front pockets.", null, "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", "Slim Chino Trousers", 58.00m },
                    { new Guid("20000000-0000-0000-0000-000000000024"), new Guid("11111111-1111-1111-1111-111111111111"), "Black", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Lightweight water-repellent bomber with ribbed cuffs and a matte zip. Layers over knitwear all season.", null, "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", "Bomber Jacket", 135.00m },
                    { new Guid("20000000-0000-0000-0000-000000000025"), new Guid("22222222-2222-2222-2222-222222222222"), "Blush", new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Fluid accordion-pleated midi with an elastic-back waistband for all-day comfort.", null, "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", "Pleated Midi Skirt", 62.00m },
                    { new Guid("20000000-0000-0000-0000-000000000026"), new Guid("22222222-2222-2222-2222-222222222222"), "Camel", new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Half-canvassed single-breasted blazer in Italian wool with natural shoulders and working cuffs.", null, "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", "Tailored Wool Blazer", 168.00m },
                    { new Guid("20000000-0000-0000-0000-000000000027"), new Guid("22222222-2222-2222-2222-222222222222"), "Champagne", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bias-cut sandwashed silk slip with adjustable straps and a subtle cowl neck.", null, "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", "Silk Slip Dress", 115.00m },
                    { new Guid("20000000-0000-0000-0000-000000000028"), new Guid("33333333-3333-3333-3333-333333333333"), "Grey", new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Chunky rib-knit lambswool beanie with a fold-over cuff. One size, generous fit.", null, "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80", "Ribbed Wool Beanie", 22.00m },
                    { new Guid("20000000-0000-0000-0000-000000000029"), new Guid("33333333-3333-3333-3333-333333333333"), "Khaki", new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Cotton-webbing belt with a brushed-metal box buckle and leather keeper. Trim to fit.", null, "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80", "Canvas Web Belt", 18.00m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nour@souqy.local", "Nour Hassan", "$2a$11$FFNhXAkvCy20R2rbAa1mFOetTfdMbfKbb9D0ZVfvAoDRh67qBzxuC", "Customer" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "omar@souqy.local", "Omar Khaled", "$2a$11$FFNhXAkvCy20R2rbAa1mFOetTfdMbfKbb9D0ZVfvAoDRh67qBzxuC", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "ColorImageUrl", "ProductId", "Size", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000002101"), "Oatmeal", "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "M", 30 },
                    { new Guid("b0000000-0000-0000-0000-000000002102"), "Charcoal", "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "L", 18 },
                    { new Guid("b0000000-0000-0000-0000-000000002201"), "White", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "M", 40 },
                    { new Guid("b0000000-0000-0000-0000-000000002202"), "Sky Blue", "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "L", 25 },
                    { new Guid("b0000000-0000-0000-0000-000000002203"), "Pink", "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "S", 12 },
                    { new Guid("b0000000-0000-0000-0000-000000002301"), "Khaki", "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "32", 22 },
                    { new Guid("b0000000-0000-0000-0000-000000002302"), "Navy", "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "34", 18 },
                    { new Guid("b0000000-0000-0000-0000-000000002303"), "Olive", "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "30", 10 },
                    { new Guid("b0000000-0000-0000-0000-000000002401"), "Black", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "M", 12 },
                    { new Guid("b0000000-0000-0000-0000-000000002402"), "Olive", "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "L", 9 },
                    { new Guid("b0000000-0000-0000-0000-000000002501"), "Blush", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "S", 15 },
                    { new Guid("b0000000-0000-0000-0000-000000002502"), "Black", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "M", 15 },
                    { new Guid("b0000000-0000-0000-0000-000000002601"), "Camel", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "M", 8 },
                    { new Guid("b0000000-0000-0000-0000-000000002602"), "Charcoal", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "S", 6 },
                    { new Guid("b0000000-0000-0000-0000-000000002701"), "Champagne", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "S", 10 },
                    { new Guid("b0000000-0000-0000-0000-000000002702"), "Emerald", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "M", 8 },
                    { new Guid("b0000000-0000-0000-0000-000000002703"), "Black", "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "L", 6 },
                    { new Guid("b0000000-0000-0000-0000-000000002801"), "Grey", "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000028"), "One Size", 60 },
                    { new Guid("b0000000-0000-0000-0000-000000002802"), "Black", "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000028"), "One Size", 45 },
                    { new Guid("b0000000-0000-0000-0000-000000002803"), "Mustard", "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000028"), "One Size", 20 },
                    { new Guid("b0000000-0000-0000-0000-000000002901"), "Khaki", "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000029"), "M", 40 },
                    { new Guid("b0000000-0000-0000-0000-000000002902"), "Navy", "https://images.unsplash.com/photo-1624222247344-550fb60583dc?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000029"), "L", 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002101"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002102"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002201"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002202"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002203"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002301"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002302"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002303"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002401"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002402"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002501"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002502"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002601"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002602"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002701"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002702"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002703"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002801"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002802"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002803"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002901"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002902"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000029"));

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000103"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000202"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000301"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000302"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000303"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000401"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000402"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000501"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000502"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000503"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000601"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000602"),
                column: "ColorImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "");
        }
    }
}
