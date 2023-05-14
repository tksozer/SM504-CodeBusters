namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteInventoryTests : TestBase
{
    [Fact]
    public async Task delete_inventory_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeInventory = new FakeInventoryBuilder().Build();
        await InsertAsync(fakeInventory);

        // Act
        var route = ApiRoutes.Inventories.Delete(fakeInventory.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}