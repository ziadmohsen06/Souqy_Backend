using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultiSizeProductVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1620799139507-2a76f79a2f4d?w=900&auto=format&fit=crop&q=80", 17 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?w=900&auto=format&fit=crop&q=80", 27 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000103"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1618354691373-d851c5c3a990?w=900&auto=format&fit=crop&q=80", 33 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1515459961680-58264ee27219?w=900&auto=format&fit=crop&q=80", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000202"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000301"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=900&auto=format&fit=crop&q=80", 20 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000302"),
                column: "StockQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000303"),
                column: "StockQuantity",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000401"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1685954134741-a699bf8807c8?w=900&auto=format&fit=crop&q=80", 12 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000402"),
                column: "StockQuantity",
                value: 16);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000601"),
                column: "Size",
                value: "One Size");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000602"),
                column: "Size",
                value: "One Size");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002101"),
                column: "StockQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002102"),
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002201"),
                column: "StockQuantity",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002202"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", 29 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002203"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002301"),
                column: "StockQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002302"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", 16 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002303"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1781106476692-ead65e434bec?w=900&auto=format&fit=crop&q=80", 23 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002401"),
                column: "StockQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002402"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", 40 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002501"),
                column: "StockQuantity",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002502"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", 13 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002601"),
                column: "StockQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002602"),
                column: "StockQuantity",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002701"),
                column: "StockQuantity",
                value: 38);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002702"),
                column: "StockQuantity",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002703"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", 9 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002901"),
                column: "Size",
                value: "One Size");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002902"),
                column: "Size",
                value: "One Size");

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "ColorImageUrl", "ProductId", "Size", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), "White", "https://images.unsplash.com/photo-1620799139507-2a76f79a2f4d?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "S", 13 },
                    { new Guid("b1000000-0000-0000-0000-000000000002"), "White", "https://images.unsplash.com/photo-1620799139507-2a76f79a2f4d?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "L", 15 },
                    { new Guid("b1000000-0000-0000-0000-000000000003"), "White", "https://images.unsplash.com/photo-1620799139507-2a76f79a2f4d?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "XL", 10 },
                    { new Guid("b1000000-0000-0000-0000-000000000004"), "Black", "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "S", 21 },
                    { new Guid("b1000000-0000-0000-0000-000000000005"), "Black", "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "M", 26 },
                    { new Guid("b1000000-0000-0000-0000-000000000006"), "Black", "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "XL", 12 },
                    { new Guid("b1000000-0000-0000-0000-000000000007"), "Navy", "https://images.unsplash.com/photo-1618354691373-d851c5c3a990?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "M", 31 },
                    { new Guid("b1000000-0000-0000-0000-000000000008"), "Navy", "https://images.unsplash.com/photo-1618354691373-d851c5c3a990?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "L", 29 },
                    { new Guid("b1000000-0000-0000-0000-000000000009"), "Navy", "https://images.unsplash.com/photo-1618354691373-d851c5c3a990?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000001"), "XL", 19 },
                    { new Guid("b1000000-0000-0000-0000-00000000000a"), "Blue", "https://images.unsplash.com/photo-1515459961680-58264ee27219?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "30", 14 },
                    { new Guid("b1000000-0000-0000-0000-00000000000b"), "Blue", "https://images.unsplash.com/photo-1515459961680-58264ee27219?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "34", 17 },
                    { new Guid("b1000000-0000-0000-0000-00000000000c"), "Blue", "https://images.unsplash.com/photo-1515459961680-58264ee27219?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "36", 13 },
                    { new Guid("b1000000-0000-0000-0000-00000000000d"), "Black", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "30", 18 },
                    { new Guid("b1000000-0000-0000-0000-00000000000e"), "Black", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "32", 35 },
                    { new Guid("b1000000-0000-0000-0000-00000000000f"), "Black", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000002"), "36", 15 },
                    { new Guid("b1000000-0000-0000-0000-000000000010"), "Red", "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "XS", 10 },
                    { new Guid("b1000000-0000-0000-0000-000000000011"), "Red", "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "M", 24 },
                    { new Guid("b1000000-0000-0000-0000-000000000012"), "Red", "https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "L", 21 },
                    { new Guid("b1000000-0000-0000-0000-000000000013"), "Blue", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "XS", 8 },
                    { new Guid("b1000000-0000-0000-0000-000000000014"), "Blue", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "S", 17 },
                    { new Guid("b1000000-0000-0000-0000-000000000015"), "Blue", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "L", 15 },
                    { new Guid("b1000000-0000-0000-0000-000000000016"), "Yellow", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "XS", 14 },
                    { new Guid("b1000000-0000-0000-0000-000000000017"), "Yellow", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "M", 35 },
                    { new Guid("b1000000-0000-0000-0000-000000000018"), "Yellow", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000003"), "L", 33 },
                    { new Guid("b1000000-0000-0000-0000-000000000019"), "Black", "https://images.unsplash.com/photo-1685954134741-a699bf8807c8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "37", 8 },
                    { new Guid("b1000000-0000-0000-0000-00000000001a"), "Black", "https://images.unsplash.com/photo-1685954134741-a699bf8807c8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "39", 12 },
                    { new Guid("b1000000-0000-0000-0000-00000000001b"), "Black", "https://images.unsplash.com/photo-1685954134741-a699bf8807c8?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "40", 6 },
                    { new Guid("b1000000-0000-0000-0000-00000000001c"), "Nude", "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "38", 26 },
                    { new Guid("b1000000-0000-0000-0000-00000000001d"), "Nude", "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "39", 25 },
                    { new Guid("b1000000-0000-0000-0000-00000000001e"), "Nude", "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", new Guid("10000000-0000-0000-0000-000000000004"), "40", 15 },
                    { new Guid("b1000000-0000-0000-0000-00000000001f"), "Oatmeal", "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "S", 13 },
                    { new Guid("b1000000-0000-0000-0000-000000000020"), "Oatmeal", "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "L", 17 },
                    { new Guid("b1000000-0000-0000-0000-000000000021"), "Oatmeal", "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "XL", 10 },
                    { new Guid("b1000000-0000-0000-0000-000000000022"), "Charcoal", "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "S", 27 },
                    { new Guid("b1000000-0000-0000-0000-000000000023"), "Charcoal", "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "M", 38 },
                    { new Guid("b1000000-0000-0000-0000-000000000024"), "Charcoal", "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000021"), "XL", 23 },
                    { new Guid("b1000000-0000-0000-0000-000000000025"), "White", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "S", 12 },
                    { new Guid("b1000000-0000-0000-0000-000000000026"), "White", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "L", 14 },
                    { new Guid("b1000000-0000-0000-0000-000000000027"), "White", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "XL", 10 },
                    { new Guid("b1000000-0000-0000-0000-000000000028"), "Sky Blue", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "S", 25 },
                    { new Guid("b1000000-0000-0000-0000-000000000029"), "Sky Blue", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "M", 37 },
                    { new Guid("b1000000-0000-0000-0000-00000000002a"), "Sky Blue", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "XL", 17 },
                    { new Guid("b1000000-0000-0000-0000-00000000002b"), "Pink", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "M", 14 },
                    { new Guid("b1000000-0000-0000-0000-00000000002c"), "Pink", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "L", 13 },
                    { new Guid("b1000000-0000-0000-0000-00000000002d"), "Pink", "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000022"), "XL", 6 },
                    { new Guid("b1000000-0000-0000-0000-00000000002e"), "Khaki", "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "30", 12 },
                    { new Guid("b1000000-0000-0000-0000-00000000002f"), "Khaki", "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "34", 22 },
                    { new Guid("b1000000-0000-0000-0000-000000000030"), "Khaki", "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "36", 14 },
                    { new Guid("b1000000-0000-0000-0000-000000000031"), "Navy", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "30", 9 },
                    { new Guid("b1000000-0000-0000-0000-000000000032"), "Navy", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "32", 15 },
                    { new Guid("b1000000-0000-0000-0000-000000000033"), "Navy", "https://images.unsplash.com/photo-1624378441864-6eda7eac51cb?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "36", 7 },
                    { new Guid("b1000000-0000-0000-0000-000000000034"), "Olive", "https://images.unsplash.com/photo-1781106476692-ead65e434bec?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "32", 35 },
                    { new Guid("b1000000-0000-0000-0000-000000000035"), "Olive", "https://images.unsplash.com/photo-1781106476692-ead65e434bec?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "34", 38 },
                    { new Guid("b1000000-0000-0000-0000-000000000036"), "Olive", "https://images.unsplash.com/photo-1781106476692-ead65e434bec?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000023"), "36", 22 },
                    { new Guid("b1000000-0000-0000-0000-000000000037"), "Black", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "S", 15 },
                    { new Guid("b1000000-0000-0000-0000-000000000038"), "Black", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "L", 20 },
                    { new Guid("b1000000-0000-0000-0000-000000000039"), "Black", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "XL", 13 },
                    { new Guid("b1000000-0000-0000-0000-00000000003a"), "Olive", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "S", 31 },
                    { new Guid("b1000000-0000-0000-0000-00000000003b"), "Olive", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "M", 41 },
                    { new Guid("b1000000-0000-0000-0000-00000000003c"), "Olive", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000024"), "XL", 19 },
                    { new Guid("b1000000-0000-0000-0000-00000000003d"), "Blush", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "XS", 11 },
                    { new Guid("b1000000-0000-0000-0000-00000000003e"), "Blush", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "M", 33 },
                    { new Guid("b1000000-0000-0000-0000-00000000003f"), "Blush", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "L", 33 },
                    { new Guid("b1000000-0000-0000-0000-000000000040"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "XS", 6 },
                    { new Guid("b1000000-0000-0000-0000-000000000041"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "S", 10 },
                    { new Guid("b1000000-0000-0000-0000-000000000042"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000025"), "L", 16 },
                    { new Guid("b1000000-0000-0000-0000-000000000043"), "Camel", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "S", 21 },
                    { new Guid("b1000000-0000-0000-0000-000000000044"), "Camel", "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "L", 28 },
                    { new Guid("b1000000-0000-0000-0000-000000000045"), "Charcoal", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "M", 20 },
                    { new Guid("b1000000-0000-0000-0000-000000000046"), "Charcoal", "https://images.unsplash.com/photo-1576995853123-5a10305d93c0?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000026"), "L", 18 },
                    { new Guid("b1000000-0000-0000-0000-000000000047"), "Champagne", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "XS", 14 },
                    { new Guid("b1000000-0000-0000-0000-000000000048"), "Champagne", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "M", 35 },
                    { new Guid("b1000000-0000-0000-0000-000000000049"), "Champagne", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "L", 29 },
                    { new Guid("b1000000-0000-0000-0000-00000000004a"), "Emerald", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "XS", 7 },
                    { new Guid("b1000000-0000-0000-0000-00000000004b"), "Emerald", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "S", 17 },
                    { new Guid("b1000000-0000-0000-0000-00000000004c"), "Emerald", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "L", 16 },
                    { new Guid("b1000000-0000-0000-0000-00000000004d"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "XS", 5 },
                    { new Guid("b1000000-0000-0000-0000-00000000004e"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "S", 9 },
                    { new Guid("b1000000-0000-0000-0000-00000000004f"), "Black", "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", new Guid("20000000-0000-0000-0000-000000000027"), "M", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "UQ_ProductVariants_Product_Color_Size",
                table: "ProductVariants",
                columns: new[] { "ProductId", "Color", "Size" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_ProductVariants_Product_Color_Size",
                table: "ProductVariants");

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000001f"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000002f"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000003f"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b1000000-0000-0000-0000-00000000004f"));

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=900&auto=format&fit=crop&q=80", 60 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=900&auto=format&fit=crop&q=80", 40 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000103"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80", 50 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000202"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80", 20 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000301"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1539109136881-3be0616acf4b?w=900&auto=format&fit=crop&q=80", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000302"),
                column: "StockQuantity",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000303"),
                column: "StockQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000401"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", 20 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000402"),
                column: "StockQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000601"),
                column: "Size",
                value: "M");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000602"),
                column: "Size",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002101"),
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002102"),
                column: "StockQuantity",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002201"),
                column: "StockQuantity",
                value: 40);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002202"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=900&auto=format&fit=crop&q=80", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002203"),
                column: "ColorImageUrl",
                value: "https://images.unsplash.com/photo-1576871337622-98d48d1cf531?w=900&auto=format&fit=crop&q=80");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002301"),
                column: "StockQuantity",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002302"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1542272604-787c3835535d?w=900&auto=format&fit=crop&q=80", 18 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002303"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1624378439575-d8705ad7ae80?w=900&auto=format&fit=crop&q=80", 10 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002401"),
                column: "StockQuantity",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002402"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=900&auto=format&fit=crop&q=80", 9 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002501"),
                column: "StockQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002502"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=900&auto=format&fit=crop&q=80", 15 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002601"),
                column: "StockQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002602"),
                column: "StockQuantity",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002701"),
                column: "StockQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002702"),
                column: "StockQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002703"),
                columns: new[] { "ColorImageUrl", "StockQuantity" },
                values: new object[] { "https://images.unsplash.com/photo-1543163521-1bf539c55dd2?w=900&auto=format&fit=crop&q=80", 6 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002901"),
                column: "Size",
                value: "M");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000002902"),
                column: "Size",
                value: "L");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductVariants_Product_Color",
                table: "ProductVariants",
                columns: new[] { "ProductId", "Color" },
                unique: true);
        }
    }
}
