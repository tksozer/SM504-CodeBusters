namespace VendomaticApi.IntegrationTests.FeatureTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.Domain.Products.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteProductCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_product_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne);
        var product = await testingServiceScope.ExecuteDbContextAsync(db => db.Products
            .FirstOrDefaultAsync(p => p.Id == fakeProductOne.Id));

        // Act
        var command = new DeleteProduct.Command(product.Id);
        await testingServiceScope.SendAsync(command);
        var productResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Products.CountAsync(p => p.Id == product.Id));

        // Assert
        productResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_product_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteProduct.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_product_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        await testingServiceScope.InsertAsync(fakeProductOne);
        var product = await testingServiceScope.ExecuteDbContextAsync(db => db.Products
            .FirstOrDefaultAsync(p => p.Id == fakeProductOne.Id));

        // Act
        var command = new DeleteProduct.Command(product.Id);
        await testingServiceScope.SendAsync(command);
        var deletedProduct = await testingServiceScope.ExecuteDbContextAsync(db => db.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == product.Id));

        // Assert
        deletedProduct?.IsDeleted.Should().BeTrue();
    }
}