namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.SharedTestHelpers.Fakes.VendingMachine;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateInventoryTests : TestBase
{
    [Fact]
    public async Task create_inventory_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeProductOne = new FakeProductBuilder().Build();
        await InsertAsync(fakeProductOne);

        var fakeVendingMachineOne = new FakeVendingMachineBuilder().Build();
        await InsertAsync(fakeVendingMachineOne);

        var fakeInventory = new FakeInventoryForCreationDto()
            .RuleFor(i => i.ProductId, _ => fakeProductOne.Id)
            .RuleFor(i => i.VendingMachineId, _ => fakeVendingMachineOne.Id).Generate();

        // Act
        var route = ApiRoutes.Inventories.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeInventory);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}