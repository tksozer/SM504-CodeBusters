namespace VendomaticApi.Domain.Products.Mappings;

using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Models;
using Mapster;

public sealed class ProductMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>();
        config.NewConfig<ProductForCreationDto, Product>()
            .TwoWays();
        config.NewConfig<ProductForUpdateDto, Product>()
            .TwoWays();
        config.NewConfig<ProductForCreation, Product>()
            .TwoWays();
        config.NewConfig<ProductForUpdate, Product>()
            .TwoWays();
    }
}