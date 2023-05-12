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


public class Product : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public int? ProductId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Type { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int Quantity { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public decimal UnitPrice { get; private set; }


    public static Product Create(ProductForCreation productForCreation)
    {
        var newProduct = new Product();

        newProduct.ProductId = productForCreation.ProductId;
        newProduct.Name = productForCreation.Name;
        newProduct.Type = productForCreation.Type;
        newProduct.Quantity = productForCreation.Quantity;
        newProduct.UnitPrice = productForCreation.UnitPrice;

        newProduct.QueueDomainEvent(new ProductCreated(){ Product = newProduct });
        
        return newProduct;
    }

    public Product Update(ProductForUpdate productForUpdate)
    {
        ProductId = productForUpdate.ProductId;
        Name = productForUpdate.Name;
        Type = productForUpdate.Type;
        Quantity = productForUpdate.Quantity;
        UnitPrice = productForUpdate.UnitPrice;

        QueueDomainEvent(new ProductUpdated(){ Id = Id });
        return this;
    }
    
    protected Product() { } // For EF + Mocking
}