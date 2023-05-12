namespace VendomaticApi.SharedTestHelpers.Fakes.Product;

using AutoBogus;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Dtos;

public sealed class FakeProductForCreationDto : AutoFaker<ProductForCreationDto>
{
    public FakeProductForCreationDto()
    {
    }
}