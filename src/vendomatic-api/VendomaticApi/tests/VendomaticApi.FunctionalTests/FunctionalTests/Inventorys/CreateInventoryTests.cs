namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventorys;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
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
        var fakeInventory = new FakeInventoryForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Inventorys.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeInventory);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}