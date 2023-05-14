namespace VendomaticApi.UnitTests.Domain.Products;

using VendomaticApi.SharedTestHelpers.Fakes.Product;
using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateProductTests
{
    private readonly Faker _faker;

    public CreateProductTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_product()
    {
        // Arrange
        var productToCreate = new FakeProductForCreation().Generate();
        
        // Act
        var fakeProduct = Product.Create(productToCreate);

        // Assert
        fakeProduct.Name.Should().Be(productToCreate.Name);
        fakeProduct.Type.Should().Be(productToCreate.Type);
        fakeProduct.Quantity.Should().Be(productToCreate.Quantity);
        fakeProduct.UnitPrice.Should().Be(productToCreate.UnitPrice);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var productToCreate = new FakeProductForCreation().Generate();
        
        // Act
        var fakeProduct = Product.Create(productToCreate);

        // Assert
        fakeProduct.DomainEvents.Count.Should().Be(1);
        fakeProduct.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ProductCreated));
    }
}