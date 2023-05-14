namespace VendomaticApi.SharedTestHelpers.Fakes.Inventory;

using AutoBogus;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Inventories.Dtos;

public sealed class FakeInventoryForCreationDto : AutoFaker<InventoryForCreationDto>
{
    public FakeInventoryForCreationDto()
    {
    }
}