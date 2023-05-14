namespace VendomaticApi.UnitTests.Domain.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Inventories.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateInventoryTests
{
    private readonly Faker _faker;

    public UpdateInventoryTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_inventory()
    {
        // Arrange
        var fakeInventory = new FakeInventoryBuilder().Build();
        var updatedInventory = new FakeInventoryForUpdate().Generate();
        
        // Act
        fakeInventory.Update(updatedInventory);

        // Assert
        fakeInventory.IsleNumber.Should().Be(updatedInventory.IsleNumber);
        fakeInventory.Quantity.Should().Be(updatedInventory.Quantity);
        fakeInventory.UnitPrice.Should().Be(updatedInventory.UnitPrice);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeInventory = new FakeInventoryBuilder().Build();
        var updatedInventory = new FakeInventoryForUpdate().Generate();
        fakeInventory.DomainEvents.Clear();
        
        // Act
        fakeInventory.Update(updatedInventory);

        // Assert
        fakeInventory.DomainEvents.Count.Should().Be(1);
        fakeInventory.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(InventoryUpdated));
    }
}