namespace VendomaticApi.IntegrationTests.FeatureTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.Domain.Inventories.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Inventories.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;

public class UpdateInventoryCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_inventory_in_db()
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
        var updatedInventoryDto = new FakeInventoryForUpdateDto()
            .RuleFor(i => i.ProductId, _ => fakeProductOne.Id)
            .RuleFor(i => i.VendingMachineId, _ => fakeVendingMachineOne.Id)
            .Generate();
        await testingServiceScope.InsertAsync(fakeInventoryOne);

        var inventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventories
            .FirstOrDefaultAsync(i => i.Id == fakeInventoryOne.Id));
        var id = inventory.Id;

        // Act
        var command = new UpdateInventory.Command(id, updatedInventoryDto);
        await testingServiceScope.SendAsync(command);
        var updatedInventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventories.FirstOrDefaultAsync(i => i.Id == id));

        // Assert
        updatedInventory.ProductId.Should().Be(updatedInventoryDto.ProductId);
        updatedInventory.VendingMachineId.Should().Be(updatedInventoryDto.VendingMachineId);
        updatedInventory.IsleNumber.Should().Be(updatedInventoryDto.IsleNumber);
        updatedInventory.Quantity.Should().Be(updatedInventoryDto.Quantity);
        updatedInventory.UnitPrice.Should().Be(updatedInventoryDto.UnitPrice);
    }
}