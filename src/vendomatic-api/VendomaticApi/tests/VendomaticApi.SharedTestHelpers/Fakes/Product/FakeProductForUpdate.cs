namespace VendomaticApi.SharedTestHelpers.Fakes.Product;

using AutoBogus;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Models;

public sealed class FakeProductForUpdate : AutoFaker<ProductForUpdate>
{
    public FakeProductForUpdate()
    {
        RuleFor(p => p.Type, f => f.PickRandom<TypeEnum>(TypeEnum.List).Name);
    }
}