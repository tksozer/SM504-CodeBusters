namespace VendomaticApi.IntegrationTests.FeatureTests.Inventorys;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.Inventorys.Features;
using SharedKernel.Exceptions;

public class AddInventoryCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_inventory_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeInventoryOne = new FakeInventoryForCreationDto().Generate();

        // Act
        var command = new AddInventory.Command(fakeInventoryOne);
        var inventoryReturned = await testingServiceScope.SendAsync(command);
        var inventoryCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventorys
            .FirstOrDefaultAsync(i => i.Id == inventoryReturned.Id));

        // Assert
        inventoryReturned.InventoryId.Should().Be(fakeInventoryOne.InventoryId);
        inventoryReturned.VendingMachineId.Should().Be(fakeInventoryOne.VendingMachineId);
        inventoryReturned.ProductId.Should().Be(fakeInventoryOne.ProductId);
        inventoryReturned.IsleNumber.Should().Be(fakeInventoryOne.IsleNumber);
        inventoryReturned.Quantity.Should().Be(fakeInventoryOne.Quantity);
        inventoryReturned.UnitPrice.Should().Be(fakeInventoryOne.UnitPrice);

        inventoryCreated.InventoryId.Should().Be(fakeInventoryOne.InventoryId);
        inventoryCreated.VendingMachineId.Should().Be(fakeInventoryOne.VendingMachineId);
        inventoryCreated.ProductId.Should().Be(fakeInventoryOne.ProductId);
        inventoryCreated.IsleNumber.Should().Be(fakeInventoryOne.IsleNumber);
        inventoryCreated.Quantity.Should().Be(fakeInventoryOne.Quantity);
        inventoryCreated.UnitPrice.Should().Be(fakeInventoryOne.UnitPrice);
    }
}