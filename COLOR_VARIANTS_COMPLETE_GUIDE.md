# Color Variants Implementation - Complete Guide

## Overview
The system has been updated to support product color variants with dedicated image URLs. Customers can now select specific colors when adding products to cart, and orders track the exact color purchased.

## Database Schema Changes

### CartItems Table (Updated)
**New Columns:**
- `ProductVariantId` (UUID, FK) - References the specific color variant
- `ProductName` (varchar(150)) - Snapshot of product name at purchase time
- `Color` (varchar(50)) - Snapshot of color variant at purchase time
- `ColorImageUrl` (varchar(500)) - Snapshot of color image URL at purchase time
- `UnitPrice` (numeric(10,2)) - Snapshot of price at purchase time

**Updated Constraints:**
- Unique constraint changed from (CartId, ProductId) to (CartId, ProductVariantId)
- Added foreign key to ProductVariants table

### OrderItems Table (Updated)
**New Column:**
- `Color` (varchar(50)) - Tracks the color of the item in the order

## DTOs (Data Transfer Objects)

### ColorVariantDto
Represents a product color variant:
```csharp
public class ColorVariantDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }              // White, Black, Brown, Blue
    public string? ColorImageUrl { get; set; }     // URL to color-specific image
    public string Size { get; set; }               // Product size
    public int StockQuantity { get; set; }         // Stock for this color
    public bool InStock => StockQuantity > 0;      // Convenience property
}
```

### ProductDto (Updated)
Now includes color variants:
```csharp
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? DefaultColor { get; set; }                    // Primary color
    public string? DefaultImageUrl { get; set; }                 // Primary image
    public DateTime CreatedAt { get; set; }
    public List<ColorVariantDto> ColorVariants { get; set; }     // NEW: All color options
}
```

### CartItemDto (Updated)
Now includes color information:
```csharp
public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ProductVariantId { get; set; }       // NEW: Specific color variant
    public string ProductName { get; set; }
    public string Color { get; set; }                // NEW: The color they selected
    public string? ColorImageUrl { get; set; }       // NEW: Image of selected color
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal => UnitPrice * Quantity;
}
```

### AddToCartDto (Updated)
Now requires specifying the color variant:
```csharp
public class AddToCartDto
{
    public Guid ProductId { get; set; }              // Product
    public Guid ProductVariantId { get; set; }       // NEW: Specific color variant
    public int Quantity { get; set; }                // Quantity of that color
}
```

### OrderItemDto (New)
Represents an item in an order:
```csharp
public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public string ProductName { get; set; }
    public string? Color { get; set; }               // Color ordered
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal => UnitPrice * Quantity;
}
```

### OrderDto (New)
Represents a complete order:
```csharp
public class OrderDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }               // Pending, Paid, Failed, Cancelled
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
```

## Service Changes

### CartService (Updated)

**GetCartAsync(userId)**
- Returns all items in user's cart with color information
- Includes ProductName, Color, ColorImageUrl for display

**AddToCartAsync(userId, AddToCartDto)**
- Now requires `ProductVariantId` to specify the color
- Validates that the specific color variant has sufficient stock
- Saves snapshot data: ProductName, Color, ColorImageUrl, UnitPrice
- If same color variant already in cart, increases quantity
- If different color of same product, creates separate cart item

**RemoveFromCartAsync(userId, cartItemId)**
- Removes specific cart item

**ClearCartAsync(userId)**
- Clears entire cart

### OrderService (New)

**CreateOrderAsync(userId, CreateOrderDto)**
- Creates order from current cart items
- Validates stock for each color variant
- Deducts stock from ProductVariants
- Captures order item snapshot with color information
- Clears cart after order creation
- Returns complete OrderDto with all items

**GetOrderAsync(userId, orderId)**
- Retrieves a specific order with all items
- Includes color information for each item

**GetUserOrdersAsync(userId)**
- Retrieves all orders for a user
- Sorted by creation date (newest first)
- Includes all order items with color info

## Frontend Integration Guide

### 1. Display Product with Color Options
```csharp
// Controller action
var product = await productService.GetProductAsync(productId);

// DTO includes:
// - ProductDto.ColorVariants[]: List of available colors
// - Each ColorVariantDto has ColorImageUrl and StockQuantity
```

**Frontend Display:**
- Show main product image (DefaultImageUrl)
- Show color selector with ColorVariantDto.ColorImageUrl for each color
- Show stock availability per color (InStock property)

### 2. Add to Cart with Color Selection
```csharp
// Frontend must collect:
var addToCartDto = new AddToCartDto
{
    ProductId = selectedProduct.Id,
    ProductVariantId = selectedColorVariant.Id,  // User selected color
    Quantity = 3
};

await cartService.AddToCartAsync(userId, addToCartDto);
```

**Key Changes:**
- Must now select a color before adding to cart
- Stock validation is per-color, not per-product
- Error if color not in stock

### 3. Display Cart
```csharp
var cartItems = await cartService.GetCartAsync(userId);

// Each item includes:
// - ProductName (e.g., "Classic T-Shirt")
// - Color (e.g., "Blue")
// - ColorImageUrl (show color-specific image)
// - UnitPrice (price at time added)
// - Quantity
// - SubTotal (calculated)
```

### 4. Checkout / Create Order
```csharp
var createOrderDto = new CreateOrderDto
{
    ShippingAddress = "123 Main St, City, State 12345"
};

var order = await orderService.CreateOrderAsync(userId, createOrderDto);
// Order automatically:
// - Deducts stock from each color variant
// - Captures color info in order items
// - Clears the cart
```

### 5. View Order
```csharp
var order = await orderService.GetOrderAsync(userId, orderId);

// Order shows:
// - Each item with: ProductName, Color, UnitPrice, Quantity
// - Total amount
// - Status
// - Shipping address
```

## Entity Relationships

```
Product (1) ──────→ (*) ProductVariant
    ↓
    └─ ColorVariants: [White, Black, Brown, Blue]
    
User (1) ──────→ (1) Cart
              ↓
          (*) CartItem (1)──→ (1) ProductVariant
                  ↓
                  └─ Snapshot: ProductName, Color, ColorImageUrl, UnitPrice

User (1) ──────→ (*) Order
              ↓
          (*) OrderItem
                  ↓
                  └─ Snapshot: ProductName, Color, UnitPrice
```

## Migration Steps

1. **Run migrations** to update database schema:
   ```bash
   dotnet ef database update -p Infrastructure -s Souqy-Backend
   ```

2. **Update controllers** to:
   - Accept ProductVariantId in add-to-cart requests
   - Return ProductDto with ColorVariants when fetching products
   - Use new OrderService for order management

3. **Update frontend** to:
   - Show color selector before adding to cart
   - Display color-specific images
   - Show per-color stock availability
   - Display color in cart and order summaries

## Example API Flow

**Step 1: Get Product Details**
```
GET /api/products/{productId}
Response: ProductDto
  - ColorVariants[4]: White, Black, Brown, Blue with images
```

**Step 2: Select Color & Add to Cart**
```
POST /api/cart/add
Body: {
  productId: "...",
  productVariantId: "...",  // The Blue variant ID
  quantity: 2
}
Response: CartItemDto (includes color info)
```

**Step 3: View Cart**
```
GET /api/cart
Response: List<CartItemDto>
  - Shows: Product name, selected color, color image URL, price per item
```

**Step 4: Checkout**
```
POST /api/orders/create
Body: { shippingAddress: "..." }
Response: OrderDto
  - Order created
  - Stock deducted for each color variant
  - Cart cleared
```

## Benefits

✅ **Professional Product Display**: Multiple colors with dedicated images
✅ **Accurate Inventory**: Stock tracked per color variant
✅ **Order History**: Customers can see exactly which color they ordered
✅ **Better UX**: Easy color selection with visual feedback
✅ **Scalable**: Ready for additional attributes (sizes, materials, etc.)

## Testing Scenarios

1. **Add same product, different colors** → Should create separate cart items
2. **Add same product, same color** → Should increase quantity
3. **Add more than available stock** → Should throw exception
4. **Checkout with multiple colors of same product** → Should create separate order items
5. **View order** → Should show correct colors for each item
