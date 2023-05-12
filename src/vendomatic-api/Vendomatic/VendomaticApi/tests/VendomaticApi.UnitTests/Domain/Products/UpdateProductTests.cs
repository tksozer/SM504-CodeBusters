namespace VendomaticApi.UnitTests.Domain.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateProductTests
{
    private readonly Faker _faker;

    public UpdateProductTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_product()
    {
        // Arrange
        var fakeProduct = new FakeProductBuilder().Build();
        var updatedProduct = new FakeProductForUpdate().Generate();
        
        // Act
        fakeProduct.Update(updatedProduct);

        // Assert
        fakeProduct.ProductId.Should().Be(updatedProduct.ProductId);
        fakeProduct.Name.Should().Be(updatedProduct.Name);
        fakeProduct.Type.Should().Be(updatedProduct.Type);
        fakeProduct.Quantity.Should().Be(updatedProduct.Quantity);
        fakeProduct.UnitPrice.Should().Be(updatedProduct.UnitPrice);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeProduct = new FakeProductBuilder().Build();
        var updatedProduct = new FakeProductForUpdate().Generate();
        fakeProduct.DomainEvents.Clear();
        
        // Act
        fakeProduct.Update(updatedProduct);

        // Assert
        fakeProduct.DomainEvents.Count.Should().Be(1);
        fakeProduct.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ProductUpdated));
    }
}