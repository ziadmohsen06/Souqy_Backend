# Quick Reference: Color Variants Implementation

## 📌 Quick Facts

| Aspect | Details |
|--------|---------|
| **Build Status** | ✅ Compiling successfully |
| **Database** | 2 migrations ready (not yet applied) |
| **Services** | CartService (refactored), OrderService (new) |
| **DTOs** | 3 new, 3 updated |
| **Entities** | 2 updated (CartItem, OrderItem) |
| **Color Options** | White, Black, Brown, Blue per product |

---

## 🚀 Key API Changes

### Before
```csharp
// Add to cart - no color selection
POST /api/cart/add
{
  "productId": "...",
  "quantity": 2
}
```

### After
```csharp
// Add to cart - must specify color variant
POST /api/cart/add
{
  "productId": "...",
  "productVariantId": "...",  // Color ID
  "quantity": 2
}
```

---

## 📦 Most Important Class Changes

### CartService - Complete Rewrite
```csharp
// OLD: .Where(c => c.UserId == userId)  ❌ Property doesn't exist
// NEW: .Carts.FirstOrDefaultAsync(c => c.UserId == userId)  ✅ Correct relationship

// OLD: if (product.Stock < quantity)  ❌ Product doesn't have Stock
// NEW: if (variant.StockQuantity < quantity)  ✅ Per-color stock
```

### OrderService - New
```csharp
// Creates order from cart
// Auto-deducts stock
// Captures color information
// Clears cart after success
```

---

## 🗄️ Database Schema - Key Changes

### CartItems Table
```sql
-- NEW COLUMNS
ProductVariantId  UUID (FK to ProductVariants)
ProductName      VARCHAR(150)     -- Snapshot
Color           VARCHAR(50)       -- Snapshot  
ColorImageUrl   VARCHAR(500)      -- Snapshot
UnitPrice       NUMERIC(10,2)     -- Snapshot

-- UPDATED CONSTRAINT
UNIQUE(CartId, ProductVariantId)  -- Was: (CartId, ProductId)
```

### OrderItems Table
```sql
-- NEW COLUMN
Color  VARCHAR(50)  -- Tracks order color
```

---

## 🔄 Entity Relationships

```
User (1) ──→ (1) Cart
              ├──→ (*) CartItem (1) ──→ (1) ProductVariant
              │        └─ Snapshots: ProductName, Color, UnitPrice, ColorImageUrl
              └──→ (*) Order (1) ──→ (*) OrderItem
                       └─ Includes: Color field
```

---

## 💾 Migration Commands

```bash
# Apply migrations to update database
dotnet ef database update -p Infrastructure -s Souqy-Backend

# Rollback (if needed)
dotnet ef database update <previous-migration-name> -p Infrastructure

# List migrations
dotnet ef migrations list -p Infrastructure
```

**Migrations to Apply:**
1. `20260907000000_AddColorVariantsToProducts` - Seeds color variants
2. `20260907000001_UpdateCartAndOrderItemsWithColorVariants` - Cart/Order updates

---

## 🧪 Quick Test

```csharp
// Get product with color options
var product = await productService.GetProductAsync(productId);
// product.ColorVariants contains [White, Black, Brown, Blue]

// Add to cart with specific color
var cartItem = await cartService.AddToCartAsync(userId, new AddToCartDto
{
    ProductId = productId,
    ProductVariantId = colorVariant.Id,  // REQUIRED - specify color
    Quantity = 2
});
// cartItem includes: Color, ColorImageUrl, UnitPrice (snapshot)

// Create order
var order = await orderService.CreateOrderAsync(userId, new CreateOrderDto
{
    ShippingAddress = "..."
});
// order.Items show color for each item
// Cart is cleared
// Stock deducted from ProductVariants
```

---

## ⚠️ Common Issues & Solutions

| Issue | Cause | Solution |
|-------|-------|----------|
| `Cart is a namespace but used like a type` | Namespace conflict | Use `new Domain.Entities.Cart` |
| `Property 'UserId' not found on CartItem` | Entity structure changed | Use `cart.UserId` instead |
| `Stock validation fails` | Checking wrong property | Use `variant.StockQuantity` not `product.Stock` |
| Color not showing | ColorImageUrl null | Ensure migration seeded ColorImageUrl |
| Duplicate colors in cart | Variant IDs different | Ensure same ColorVariant is used |

---

## 📚 Documentation Files

| File | Purpose |
|------|---------|
| [COLOR_VARIANTS_DOCUMENTATION.md](COLOR_VARIANTS_DOCUMENTATION.md) | Initial color variant setup |
| [COLOR_VARIANTS_COMPLETE_GUIDE.md](COLOR_VARIANTS_COMPLETE_GUIDE.md) | Comprehensive guide with examples |
| [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) | Detailed change documentation |
| [QUICK_REFERENCE.md](QUICK_REFERENCE.md) | This file - quick lookups |

---

## 🔗 File Locations

### Services
- [CartService.cs](Application/Features/Cart/Service/CartService.cs) - Cart management
- [OrderService.cs](Application/Features/Orders/Service/OrderService.cs) - Order management

### DTOs
- [ProductDto.cs](Application/Features/Products/DTOs/ProductDto.cs)
- [CartItemDto.cs](Application/Features/Cart/DTOs/CartItemDto.cs)
- [AddToCartDto.cs](Application/Features/Cart/DTOs/AddToCartDto.cs)
- [ColorVariantDto.cs](Application/Features/Products/DTOs/ColorVariantDto.cs)
- [OrderDto.cs](Application/Features/Orders/DTOs/OrderDto.cs)
- [OrderItemDto.cs](Application/Features/Orders/DTOs/OrderItemDto.cs)

### Entities
- [CartItem.cs](Domain/Entities/CartItem.cs)
- [OrderItem.cs](Domain/Entities/OrderItem.cs)

### Configuration
- [MapsterConfig.cs](Application/Mapping/MapsterConfig.cs)
- [ApplicationDbContext.cs](Infrastructure/ApplicationDbContext.cs)
- [DI.cs](Application/DI.cs)

---

## ✅ Checklist for Using Color Variants

- [ ] Migrations applied (`dotnet ef database update`)
- [ ] Test CartService.AddToCartAsync with ProductVariantId
- [ ] Verify ProductDto returns ColorVariants
- [ ] Update cart API to send ProductVariantId
- [ ] Update product API response
- [ ] Create OrderController endpoints
- [ ] Test complete flow: Product → Color → Cart → Order
- [ ] Frontend shows color selector with images
- [ ] Frontend displays color in cart
- [ ] Frontend shows order color history

---

## 🎯 For API Controller Implementation

### Product Endpoint
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
{
    // Returns ProductDto with:
    // - Name, Description, Price
    // - DefaultColor, DefaultImageUrl
    // - ColorVariants[]: List of ColorVariantDto
    //   Each includes: Id, Color, ColorImageUrl, StockQuantity, InStock
}
```

### Cart Endpoint
```csharp
[HttpPost("add")]
public async Task<ActionResult<CartItemDto>> AddToCart(AddToCartDto dto)
{
    // NOW REQUIRES:
    // - ProductId
    // - ProductVariantId (color selection - NOT optional)
    // - Quantity

    // Returns CartItemDto with:
    // - ProductName, Color, ColorImageUrl
    // - UnitPrice (snapshot), Quantity
    // - SubTotal
}
```

### Order Endpoint (New)
```csharp
[HttpPost]
public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto dto)
{
    // Creates order from cart
    // Deducts stock
    // Clears cart
    // Returns OrderDto with order items including colors
}

[HttpGet("{id}")]
public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
{
    // Returns OrderDto with items showing colors
}
```

---

**Last Updated:** 2026-09-07
**Status:** Implementation Complete ✅
**Next:** Apply migrations and update controllers
