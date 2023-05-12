namespace VendomaticApi.FunctionalTests.FunctionalTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetProductTests : TestBase
{
    [Fact]
    public async Task get_product_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProductBuilder().Build();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.GetRecord(fakeProduct.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}