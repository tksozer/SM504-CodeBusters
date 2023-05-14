namespace VendomaticApi.Domain.Products;

using SharedKernel.Exceptions;
using VendomaticApi.Domain.Products.Models;
using VendomaticApi.Domain.Products.DomainEvents;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using VendomaticApi.Domain.Inventories;


public class Product : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; private set; }

    private TypeEnum _type;
    [Sieve(CanFilter = true, CanSort = true)]
    public string Type
    {
        get => _type.Name;
        private set
        {
            if (!TypeEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Type), value);

            _type = parsed;
        }
    }

    [Sieve(CanFilter = true, CanSort = true)]
    public int Quantity { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public decimal UnitPrice { get; private set; }

    [JsonIgnore, IgnoreDataMember]
    public ICollection<Inventory> Inventories { get; private set; }


    public static Product Create(ProductForCreation productForCreation)
    {
        var newProduct = new Product();

        newProduct.Name = productForCreation.Name;
        newProduct.Type = productForCreation.Type;
        newProduct.Quantity = productForCreation.Quantity;
        newProduct.UnitPrice = productForCreation.UnitPrice;

        newProduct.QueueDomainEvent(new ProductCreated(){ Product = newProduct });
        
        return newProduct;
    }

    public Product Update(ProductForUpdate productForUpdate)
    {
        Name = productForUpdate.Name;
        Type = productForUpdate.Type;
        Quantity = productForUpdate.Quantity;
        UnitPrice = productForUpdate.UnitPrice;

        QueueDomainEvent(new ProductUpdated(){ Id = Id });
        return this;
    }
    
    protected Product() { } // For EF + Mocking
}