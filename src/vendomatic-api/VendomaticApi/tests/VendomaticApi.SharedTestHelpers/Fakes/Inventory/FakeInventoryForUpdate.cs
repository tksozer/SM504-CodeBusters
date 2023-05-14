namespace VendomaticApi.SharedTestHelpers.Fakes.Inventory;

using AutoBogus;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Inventories.Models;

public sealed class FakeInventoryForUpdate : AutoFaker<InventoryForUpdate>
{
    public FakeInventoryForUpdate()
    {
    }
}