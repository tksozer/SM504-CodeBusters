namespace VendomaticApi.IntegrationTests.FeatureTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.Domain.Products.Dtos;
using SharedKernel.Exceptions;
using VendomaticApi.Domain.Products.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateProductCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_product_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductBuilder().Build();
        var updatedProductDto = new FakeProductForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeProductOne);

        var product = await testingServiceScope.ExecuteDbContextAsync(db => db.Products
            .FirstOrDefaultAsync(p => p.Id == fakeProductOne.Id));
        var id = product.Id;

        // Act
        var command = new UpdateProduct.Command(id, updatedProductDto);
        await testingServiceScope.SendAsync(command);
        var updatedProduct = await testingServiceScope.ExecuteDbContextAsync(db => db.Products.FirstOrDefaultAsync(p => p.Id == id));

        // Assert
        updatedProduct.ProductId.Should().Be(updatedProductDto.ProductId);
        updatedProduct.Name.Should().Be(updatedProductDto.Name);
        updatedProduct.Type.Should().Be(updatedProductDto.Type);
        updatedProduct.Quantity.Should().Be(updatedProductDto.Quantity);
        updatedProduct.UnitPrice.Should().Be(updatedProductDto.UnitPrice);
    }
}