namespace VendomaticApi.SharedTestHelpers.Fakes.Inventory;

using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Models;

public class FakeInventoryBuilder
{
    private InventoryForCreation _creationData = new FakeInventoryForCreation().Generate();

    public FakeInventoryBuilder WithModel(InventoryForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeInventoryBuilder WithInventoryId(int? inventoryId)
    {
        _creationData.InventoryId = inventoryId;
        return this;
    }
    
    public FakeInventoryBuilder WithVendingMachineId(int vendingMachineId)
    {
        _creationData.VendingMachineId = vendingMachineId;
        return this;
    }
    
    public FakeInventoryBuilder WithProductId(int productId)
    {
        _creationData.ProductId = productId;
        return this;
    }
    
    public FakeInventoryBuilder WithIsleNumber(int isleNumber)
    {
        _creationData.IsleNumber = isleNumber;
        return this;
    }
    
    public FakeInventoryBuilder WithQuantity(int quantity)
    {
        _creationData.Quantity = quantity;
        return this;
    }
    
    public FakeInventoryBuilder WithUnitPrice(decimal unitPrice)
    {
        _creationData.UnitPrice = unitPrice;
        return this;
    }
    
    public Inventory Build()
    {
        var result = Inventory.Create(_creationData);
        return result;
    }
}