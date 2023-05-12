namespace VendomaticApi.IntegrationTests.FeatureTests.Inventorys;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.Domain.Inventorys.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteInventoryCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_inventory_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeInventoryOne = new FakeInventoryBuilder().Build();
        await testingServiceScope.InsertAsync(fakeInventoryOne);
        var inventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventorys
            .FirstOrDefaultAsync(i => i.Id == fakeInventoryOne.Id));

        // Act
        var command = new DeleteInventory.Command(inventory.Id);
        await testingServiceScope.SendAsync(command);
        var inventoryResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventorys.CountAsync(i => i.Id == inventory.Id));

        // Assert
        inventoryResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_inventory_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteInventory.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_inventory_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeInventoryOne = new FakeInventoryBuilder().Build();
        await testingServiceScope.InsertAsync(fakeInventoryOne);
        var inventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventorys
            .FirstOrDefaultAsync(i => i.Id == fakeInventoryOne.Id));

        // Act
        var command = new DeleteInventory.Command(inventory.Id);
        await testingServiceScope.SendAsync(command);
        var deletedInventory = await testingServiceScope.ExecuteDbContextAsync(db => db.Inventorys
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == inventory.Id));

        // Assert
        deletedInventory?.IsDeleted.Should().BeTrue();
    }
}