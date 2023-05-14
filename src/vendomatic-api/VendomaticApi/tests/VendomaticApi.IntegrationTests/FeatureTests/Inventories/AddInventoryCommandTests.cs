namespace VendomaticApi.IntegrationTests.FeatureTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.Inventories.Features;
using SharedKernel.Exceptions;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

public class AddInventoryCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_inventory_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);

        var fakeInventoryOne = new FakeInventoryForCreationDto()
            .RuleFor(i => i.ProductId, _ => fakeProductOne.Id)
            .RuleFor(i => i.VendingMachineId, _ => fakeVendingMachineOne.Id).Generate();

        // Act
        var command = new AddInventory.Command(fakeInventoryOne);
        var inventoryReturned = await testingServiceScope.SendAsync(command);
        var inventoryCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventories
            .FirstOrDefaultAsync(i => i.Id == inventoryReturned.Id));

        // Assert
        inventoryReturned.ProductId.Should().Be(fakeInventoryOne.ProductId);
        inventoryReturned.VendingMachineId.Should().Be(fakeInventoryOne.VendingMachineId);
        inventoryReturned.IsleNumber.Should().Be(fakeInventoryOne.IsleNumber);
        inventoryReturned.Quantity.Should().Be(fakeInventoryOne.Quantity);
        inventoryReturned.UnitPrice.Should().Be(fakeInventoryOne.UnitPrice);

        inventoryCreated.ProductId.Should().Be(fakeInventoryOne.ProductId);
        inventoryCreated.VendingMachineId.Should().Be(fakeInventoryOne.VendingMachineId);
        inventoryCreated.IsleNumber.Should().Be(fakeInventoryOne.IsleNumber);
        inventoryCreated.Quantity.Should().Be(fakeInventoryOne.Quantity);
        inventoryCreated.UnitPrice.Should().Be(fakeInventoryOne.UnitPrice);
    }
}