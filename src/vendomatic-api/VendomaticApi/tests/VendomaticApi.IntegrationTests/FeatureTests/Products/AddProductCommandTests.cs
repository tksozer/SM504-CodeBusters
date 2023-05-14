namespace VendomaticApi.IntegrationTests.FeatureTests.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using VendomaticApi.Domain.Products.Features;
using SharedKernel.Exceptions;

public class AddProductCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_product_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeProductOne = new FakeProductForCreationDto().Generate();

        // Act
        var command = new AddProduct.Command(fakeProductOne);
        var productReturned = await testingServiceScope.SendAsync(command);
        var productCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Products
            .FirstOrDefaultAsync(p => p.Id == productReturned.Id));

        // Assert
        productReturned.Name.Should().Be(fakeProductOne.Name);
        productReturned.Type.Should().Be(fakeProductOne.Type);
        productReturned.Quantity.Should().Be(fakeProductOne.Quantity);
        productReturned.UnitPrice.Should().Be(fakeProductOne.UnitPrice);

        productCreated.Name.Should().Be(fakeProductOne.Name);
        productCreated.Type.Should().Be(fakeProductOne.Type);
        productCreated.Quantity.Should().Be(fakeProductOne.Quantity);
        productCreated.UnitPrice.Should().Be(fakeProductOne.UnitPrice);
    }
}