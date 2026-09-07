# Product Color Variants Implementation

## Overview
Each product now has **4 color variants** (White, Black, Brown, Blue) with dedicated image URLs for professional product display.

## Database Schema Changes

### ProductVariants Table (Enhanced)
Added two new columns:
- **Color** (varchar(50), NOT NULL): The color of the variant (White, Black, Brown, Blue)
- **ColorImageUrl** (varchar(500), nullable): URL to the color-specific product image

### Products Table (Simplified)
Removed:
- **StockQuantity**: Now managed at the variant level
- **Size**: Now managed at the variant level

The original `Color` and `ImageUrl` columns remain for backward compatibility (representing the primary/default color).

## Data Model

### ProductVariant Entity
```csharp
public class ProductVariant
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }              // NEW
    public string? ColorImageUrl { get; set set;   // NEW
    public int StockQuantity { get; set; }
    public Product Product { get; set; }
}
```

## Usage Examples

### Retrieving All Color Variants for a Product
```csharp
var productWithVariants = await _context.Products
    .Include(p => p.Variants)
    .FirstOrDefaultAsync(p => p.Id == productId);

foreach (var variant in productWithVariants.Variants)
{
    Console.WriteLine($"{variant.Color} - Stock: {variant.StockQuantity}");
    Console.WriteLine($"Image URL: {variant.ColorImageUrl}");
}
```

### Retrieving a Specific Color Variant
```csharp
var blueVariant = await _context.ProductVariants
    .FirstOrDefaultAsync(v => v.ProductId == productId && v.Color == "Blue");

if (blueVariant != null)
{
    Console.WriteLine($"Available: {blueVariant.StockQuantity}");
    Console.WriteLine($"Image: {blueVariant.ColorImageUrl}");
}
```

### Getting Color Variants for Display
```csharp
var colorOptions = await _context.ProductVariants
    .Where(v => v.ProductId == productId)
    .Select(v => new {
        Color = v.Color,
        ImageUrl = v.ColorImageUrl,
        InStock = v.StockQuantity > 0
    })
    .ToListAsync();
```

## Seeded Data
Each product has 4 color variants:

| Product | Colors | Stock (per color) | Example |
|---------|--------|------------------|---------|
| Classic T-Shirt | White, Black, Brown, Blue | Varies | 100, 80, 60, 90 |
| Denim Jeans | White, Black, Brown, Blue | Varies | 30, 50, 20, 40 |
| Summer Dress | White, Black, Brown, Blue | Varies | 25, 20, 15, 40 |
| Heels | White, Black, Brown, Blue | Varies | 15, 25, 10, 18 |
| Baseball Cap | White, Black, Brown, Blue | Varies | 150, 200, 100, 180 |
| Leather Belt | White, Black, Brown, Blue | Varies | 40, 80, 70, 50 |

## Frontend Integration
When building the product detail page:
1. Load the product
2. Fetch all ProductVariants for that product
3. Display color options with the ColorImageUrl images
4. Allow users to select a color before adding to cart
5. Update stock availability based on selected color variant

## Migration
Run the migration to apply changes:
```bash
dotnet ef database update -p Infrastructure -s Souqy-Backend
```

## Notes
- Unique constraint on (ProductId, Color) ensures no duplicate colors per product
- Stock is now per variant, not per product - enabling accurate inventory management per color
- Size information is retained for future multi-size variant support
