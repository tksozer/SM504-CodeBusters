namespace VendomaticApi.IntegrationTests.FeatureTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.Domain.Products.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class ProductQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_product_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne);

        // Act
        var query = new GetProduct.Query(fakeProductOne.Id);
        var product = await testingServiceScope.SendAsync(query);

        // Assert
        product.ProductId.Should().Be(fakeProductOne.ProductId);
        product.Name.Should().Be(fakeProductOne.Name);
        product.Type.Should().Be(fakeProductOne.Type);
        product.Quantity.Should().Be(fakeProductOne.Quantity);
        product.UnitPrice.Should().Be(fakeProductOne.UnitPrice);
    }

    [Fact]
    public async Task get_product_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetProduct.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}