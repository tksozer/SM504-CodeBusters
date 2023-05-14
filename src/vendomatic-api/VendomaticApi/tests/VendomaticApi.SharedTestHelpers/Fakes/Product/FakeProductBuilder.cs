namespace VendomaticApi.SharedTestHelpers.Fakes.Product;

using VendomaticApi.Domain.Products;
using VendomaticApi.Domain.Products.Models;

public class FakeProductBuilder
{
    private ProductForCreation _creationData = new FakeProductForCreation().Generate();

    public FakeProductBuilder WithModel(ProductForCreation model)
    {
        _creationData = model;
        return this;
    }
    
    public FakeProductBuilder WithName(string name)
    {
        _creationData.Name = name;
        return this;
    }
    
    public FakeProductBuilder WithType(string type)
    {
        _creationData.Type = type;
        return this;
    }
    
    public FakeProductBuilder WithQuantity(int quantity)
    {
        _creationData.Quantity = quantity;
        return this;
    }
    
    public FakeProductBuilder WithUnitPrice(decimal unitPrice)
    {
        _creationData.UnitPrice = unitPrice;
        return this;
    }
    
    public Product Build()
    {
        var result = Product.Create(_creationData);
        return result;
    }
}