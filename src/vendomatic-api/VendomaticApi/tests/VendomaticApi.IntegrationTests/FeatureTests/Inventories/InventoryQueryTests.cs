namespace VendomaticApi.IntegrationTests.FeatureTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.Domain.Inventories.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

public class InventoryQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_inventory_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        await testingServiceScope.InsertAsync(fakeVendingMachineOne);

        var fakeInventoryOne = new FakeInventoryBuilder()
            .WithProductId(fakeProductOne.Id)
            .WithVendingMachineId(fakeVendingMachineOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeInventoryOne);

        // Act
        var query = new GetInventory.Query(fakeInventoryOne.Id);
        var inventory = await testingServiceScope.SendAsync(query);

        // Assert
        inventory.ProductId.Should().Be(fakeInventoryOne.ProductId);
        inventory.VendingMachineId.Should().Be(fakeInventoryOne.VendingMachineId);
        inventory.IsleNumber.Should().Be(fakeInventoryOne.IsleNumber);
        inventory.Quantity.Should().Be(fakeInventoryOne.Quantity);
        inventory.UnitPrice.Should().Be(fakeInventoryOne.UnitPrice);
    }

    [Fact]
    public async Task get_inventory_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetInventory.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}