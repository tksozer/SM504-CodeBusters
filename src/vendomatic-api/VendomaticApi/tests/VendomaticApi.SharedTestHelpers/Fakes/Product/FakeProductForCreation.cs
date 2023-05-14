namespace VendomaticApi.SharedTestHelpers.Fakes.Product;

using AutoBogus;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Models;

public sealed class FakeProductForCreation : AutoFaker<ProductForCreation>
{
    public FakeProductForCreation()
    {
        RuleFor(p => p.Type, f => f.PickRandom<TypeEnum>(TypeEnum.List).Name);
    }
}