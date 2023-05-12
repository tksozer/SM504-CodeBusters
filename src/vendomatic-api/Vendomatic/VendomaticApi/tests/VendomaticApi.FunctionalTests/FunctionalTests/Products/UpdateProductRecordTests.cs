namespace VendomaticApi.FunctionalTests.FunctionalTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateProductRecordTests : TestBase
{
    [Fact]
    public async Task put_product_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProductBuilder().Build();
        var updatedProductDto = new FakeProductForUpdateDto().Generate();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.Put(fakeProduct.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedProductDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}