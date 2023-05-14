namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateInventoryRecordTests : TestBase
{
    [Fact]
    public async Task put_inventory_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeProductOne = new FakeProductBuilder().Build();
        await InsertAsync(fakeProductOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        await InsertAsync(fakeVendingMachineOne);

        var fakeInventory = new FakeInventoryBuilder()
            .WithProductId(fakeProductOne.Id)
            .WithVendingMachineId(fakeVendingMachineOne.Id).Build();
        var updatedInventoryDto = new FakeInventoryForUpdateDto()
            .RuleFor(i => i.ProductId, _ => fakeProductOne.Id)
            .RuleFor(i => i.VendingMachineId, _ => fakeVendingMachineOne.Id)
            .Generate();
        await InsertAsync(fakeInventory);

        // Act
        var route = ApiRoutes.Inventories.Put(fakeInventory.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedInventoryDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}