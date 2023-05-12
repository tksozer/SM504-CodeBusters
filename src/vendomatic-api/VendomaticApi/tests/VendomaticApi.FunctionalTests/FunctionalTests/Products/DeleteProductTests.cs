namespace VendomaticApi.FunctionalTests.FunctionalTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteProductTests : TestBase
{
    [Fact]
    public async Task delete_product_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProductBuilder().Build();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.Delete(fakeProduct.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}