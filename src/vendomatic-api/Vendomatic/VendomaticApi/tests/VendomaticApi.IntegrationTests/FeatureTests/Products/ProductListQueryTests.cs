namespace VendomaticApi.IntegrationTests.FeatureTests.Products;

using VendomaticApi.Domain.Products.Dtos;
using VendomaticApi.SharedTestHelpers.Fakes.Product;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Products.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class ProductListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_product_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        var fakeProductTwo = new FakeProductBuilder().Build();
        var queryParameters = new ProductParametersDto();

        await testingServiceScope.InsertAsync(fakeProductOne, fakeProductTwo);

        // Act
        var query = new GetProductList.Query(queryParameters);
        var products = await testingServiceScope.SendAsync(query);

        // Assert
        products.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}