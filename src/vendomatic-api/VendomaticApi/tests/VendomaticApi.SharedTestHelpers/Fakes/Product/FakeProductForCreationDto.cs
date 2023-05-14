namespace VendomaticApi.SharedTestHelpers.Fakes.Product;

using AutoBogus;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Dtos;

public sealed class FakeProductForCreationDto : AutoFaker<ProductForCreationDto>
{
    public FakeProductForCreationDto()
    {
        RuleFor(p => p.Type, f => f.PickRandom<TypeEnum>(TypeEnum.List).Name);
    }
}