using Mapster;
using Souqy.Features.Products;
using Souqy.Features.Products.Domain;

namespace Souqy.Mapping
{
    public static class MapsterConfig
    {
        public static void Register()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            // Product -> ProductDto
            config.NewConfig<Product, ProductDto>();

            // CreateProductDto -> Product (maps Name, Description, Price)
            config.NewConfig<CreateProductDto, Product>();

            // UpdateProductDto -> Product (apply property values onto existing Product)
            config.NewConfig<UpdateProductDto, Product>()
                .IgnoreNullValues(true);
        }
    }
}
