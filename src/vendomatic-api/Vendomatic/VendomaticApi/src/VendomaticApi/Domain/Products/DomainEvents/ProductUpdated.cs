namespace VendomaticApi.Domain.Products.DomainEvents;

public sealed class ProductUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            