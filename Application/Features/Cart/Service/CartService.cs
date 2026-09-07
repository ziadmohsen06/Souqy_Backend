using Application.Features.Cart.DTOs;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using CartEntity = Domain.Entities.Cart;

namespace Application.Features.Cart.Service
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<CartEntity> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new CartEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<List<CartItemDto>> GetCartAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new List<CartItemDto>();
            }

            return cart.Items.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductVariantId = ci.ProductVariantId,
                ProductName = ci.ProductName,
                Color = ci.Color,
                ColorImageUrl = ci.ColorImageUrl,
                UnitPrice = ci.UnitPrice,
                Quantity = ci.Quantity
            }).ToList();
        }

        public async Task<CartItemDto> AddToCartAsync(Guid userId, AddToCartDto dto)
        {
            // Validate product exists
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Validate product variant exists and has stock
            var variant = await _context.ProductVariants.FindAsync(dto.ProductVariantId);
            if (variant == null)
            {
                throw new KeyNotFoundException("Product color variant not found.");
            }

            if (variant.StockQuantity < dto.Quantity)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for '{product.Name}' in {variant.Color}. Available: {variant.StockQuantity}");
            }

            // Get or create user's cart
            var cart = await GetOrCreateCartAsync(userId);

            // Check if product variant already in cart
            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductVariantId == dto.ProductVariantId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                cart.UpdatedAt = DateTime.UtcNow;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    ProductVariantId = variant.Id,
                    ProductName = product.Name,
                    Color = variant.Color,
                    ColorImageUrl = variant.ColorImageUrl,
                    UnitPrice = product.Price,
                    Quantity = dto.Quantity
                };
                cart.UpdatedAt = DateTime.UtcNow;
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            var item = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductVariantId == dto.ProductVariantId && ci.CartId == cart.Id);

            return new CartItemDto
            {
                Id = item!.Id,
                ProductId = item.ProductId,
                ProductVariantId = item.ProductVariantId,
                ProductName = item.ProductName,
                Color = item.Color,
                ColorImageUrl = item.ColorImageUrl,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            };
        }

        public async Task<bool> RemoveFromCartAsync(Guid userId, Guid cartItemId)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);

            if (cartItem == null || cartItem.Cart == null || cartItem.Cart.UserId != userId)
            {
                return false;
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null && cart.Items.Count > 0)
            {
                _context.CartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
            }
        }
    }
}
