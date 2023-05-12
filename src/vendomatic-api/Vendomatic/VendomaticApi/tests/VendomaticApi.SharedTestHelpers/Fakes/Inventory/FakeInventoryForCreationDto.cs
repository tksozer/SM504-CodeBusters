namespace VendomaticApi.SharedTestHelpers.Fakes.Inventory;

using AutoBogus;
using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Dtos;

public sealed class FakeInventoryForCreationDto : AutoFaker<InventoryForCreationDto>
{
    public FakeInventoryForCreationDto()
    {
    }
}