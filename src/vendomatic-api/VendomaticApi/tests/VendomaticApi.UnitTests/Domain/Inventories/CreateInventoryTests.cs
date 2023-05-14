namespace VendomaticApi.UnitTests.Domain.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Inventories.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateInventoryTests
{
    private readonly Faker _faker;

    public CreateInventoryTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_inventory()
    {
        // Arrange
        var inventoryToCreate = new FakeInventoryForCreation().Generate();
        
        // Act
        var fakeInventory = Inventory.Create(inventoryToCreate);

        // Assert
        fakeInventory.ProductId.Should().Be(inventoryToCreate.ProductId);
        fakeInventory.VendingMachineId.Should().Be(inventoryToCreate.VendingMachineId);
        fakeInventory.IsleNumber.Should().Be(inventoryToCreate.IsleNumber);
        fakeInventory.Quantity.Should().Be(inventoryToCreate.Quantity);
        fakeInventory.UnitPrice.Should().Be(inventoryToCreate.UnitPrice);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var inventoryToCreate = new FakeInventoryForCreation().Generate();
        
        // Act
        var fakeInventory = Inventory.Create(inventoryToCreate);

        // Assert
        fakeInventory.DomainEvents.Count.Should().Be(1);
        fakeInventory.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(InventoryCreated));
    }
}