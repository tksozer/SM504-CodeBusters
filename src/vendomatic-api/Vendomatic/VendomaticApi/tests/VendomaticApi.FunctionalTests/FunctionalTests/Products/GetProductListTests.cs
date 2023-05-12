namespace VendomaticApi.FunctionalTests.FunctionalTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetProductListTests : TestBase
{
    [Fact]
    public async Task get_product_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Products.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}