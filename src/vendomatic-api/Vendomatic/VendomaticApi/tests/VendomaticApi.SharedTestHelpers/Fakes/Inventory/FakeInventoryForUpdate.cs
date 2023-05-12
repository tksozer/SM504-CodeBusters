namespace VendomaticApi.SharedTestHelpers.Fakes.Inventory;

using AutoBogus;
using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Models;

public sealed class FakeInventoryForUpdate : AutoFaker<InventoryForUpdate>
{
    public FakeInventoryForUpdate()
    {
    }
}