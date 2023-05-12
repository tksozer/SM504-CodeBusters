namespace VendomaticApi.FunctionalTests.FunctionalTests.Inventorys;

using VendomaticApi.SharedTestHelpers.Fakes.Inventory;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetInventoryListTests : TestBase
{
    [Fact]
    public async Task get_inventory_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Inventorys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}