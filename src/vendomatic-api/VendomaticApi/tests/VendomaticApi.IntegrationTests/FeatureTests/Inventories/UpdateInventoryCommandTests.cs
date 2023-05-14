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

public class UpdateInventoryCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_inventory_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeInventoryOne = new FakeInventoryBuilder().Build();
        var updatedInventoryDto = new FakeInventoryForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeInventoryOne);

        var inventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventories
            .FirstOrDefaultAsync(i => i.Id == fakeInventoryOne.Id));
        var id = inventory.Id;

        // Act
        var command = new UpdateInventory.Command(id, updatedInventoryDto);
        await testingServiceScope.SendAsync(command);
        var updatedInventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventories.FirstOrDefaultAsync(i => i.Id == id));

        // Assert
        updatedInventory.IsleNumber.Should().Be(updatedInventoryDto.IsleNumber);
        updatedInventory.Quantity.Should().Be(updatedInventoryDto.Quantity);
        updatedInventory.UnitPrice.Should().Be(updatedInventoryDto.UnitPrice);
    }
}