using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartAndOrderItemsWithColorVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add columns to CartItems
            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantId",
                table: "CartItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartItems",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "CartItems",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorImageUrl",
                table: "CartItems",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "CartItems",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            // Add Color column to OrderItems
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderItems",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            // Drop old unique constraint on CartItems
            migrationBuilder.DropIndex(
                name: "UQ_CartItems_Cart_Product",
                table: "CartItems");

            // Create new unique constraint on CartItems with ProductVariant
            migrationBuilder.CreateIndex(
                name: "UQ_CartItems_Cart_ProductVariant",
                table: "CartItems",
                columns: new[] { "CartId", "ProductVariantId" },
                unique: true);

            // Add foreign key for ProductVariant
            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariants",
                table: "CartItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariants",
                table: "CartItems");

            // Drop new unique constraint
            migrationBuilder.DropIndex(
                name: "UQ_CartItems_Cart_ProductVariant",
                table: "CartItems");

            // Remove columns from CartItems
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ColorImageUrl",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "CartItems");

            // Remove Color column from OrderItems
            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderItems");

            // Recreate old unique constraint
            migrationBuilder.CreateIndex(
                name: "UQ_CartItems_Cart_Product",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);
        }
    }
}
