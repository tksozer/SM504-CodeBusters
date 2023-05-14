namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventories;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
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
        var fakeInventory = new FakeInventoryBuilder().Build();
        var updatedInventoryDto = new FakeInventoryForUpdateDto().Generate();
        await InsertAsync(fakeInventory);

        // Act
        var route = ApiRoutes.Inventories.Put(fakeInventory.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedInventoryDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}