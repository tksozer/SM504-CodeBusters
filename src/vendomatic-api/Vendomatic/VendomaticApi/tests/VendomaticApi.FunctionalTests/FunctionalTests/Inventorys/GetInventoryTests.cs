namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventorys;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
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
        var fakeInventory = new FakeInventoryBuilder().Build();
        await InsertAsync(fakeInventory);

        // Act
        var route = ApiRoutes.Inventorys.GetRecord(fakeInventory.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}