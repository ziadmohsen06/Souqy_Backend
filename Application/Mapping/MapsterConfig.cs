using Mapster;
using Application.Features.Products.DTOs;
using Application.Features.Orders.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    public static class MapsterConfig
    {
        public static void Register()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            // ProductVariant -> ColorVariantDto
            config.NewConfig<ProductVariant, ColorVariantDto>()
                .Map(dest => dest.InStock, src => src.StockQuantity > 0);

            // ProductVariant -> ProductVariantDto (InStock is a computed getter)
            config.NewConfig<ProductVariant, ProductVariantDto>();

            // Product -> ProductDto with color variants
            config.NewConfig<Product, ProductDto>()
                .Map(dest => dest.DefaultColor, src => src.Color)
                .Map(dest => dest.DefaultImageUrl, src => src.ImageUrl)
                .Map(dest => dest.ColorVariants, src => src.Variants);

            // CreateProductDto -> Product (maps Name, Description, Price)
            config.NewConfig<CreateProductDto, Product>();

            // UpdateProductDto -> Product (apply property values onto existing Product)
            config.NewConfig<UpdateProductDto, Product>()
                .IgnoreNullValues(true);

            // Order -> OrderDto
            config.NewConfig<Order, OrderDto>();

            // OrderItem -> OrderItemDto
            config.NewConfig<OrderItem, OrderItemDto>();
        }
    }
}
