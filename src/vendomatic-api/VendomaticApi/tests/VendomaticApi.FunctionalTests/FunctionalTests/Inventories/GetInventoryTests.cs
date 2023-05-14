namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetInventoryTests : TestBase
{
    [Fact]
    public async Task get_inventory_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeProductOne = new FakeProductBuilder().Build();
        await InsertAsync(fakeProductOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        await InsertAsync(fakeVendingMachineOne);

        var fakeInventory = new FakeInventoryBuilder()
            .WithProductId(fakeProductOne.Id)
            .WithVendingMachineId(fakeVendingMachineOne.Id).Build();
        await InsertAsync(fakeInventory);

        // Act
        var route = ApiRoutes.Inventories.GetRecord(fakeInventory.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}