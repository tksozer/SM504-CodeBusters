namespace VendomaticApi.FunctionalTests.FunctionalTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateProductTests : TestBase
{
    [Fact]
    public async Task create_product_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeProduct = new FakeProductForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Products.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeProduct);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}