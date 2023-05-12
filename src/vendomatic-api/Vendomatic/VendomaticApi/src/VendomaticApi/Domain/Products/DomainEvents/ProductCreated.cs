namespace VendomaticApi.Domain.Products.DomainEvents;

public sealed class ProductCreated : DomainEvent
{
    public Product Product { get; set; } 
}
            