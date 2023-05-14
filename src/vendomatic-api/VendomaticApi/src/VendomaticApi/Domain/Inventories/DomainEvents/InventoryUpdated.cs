namespace VendomaticApi.Domain.Inventories.DomainEvents;

public sealed class InventoryUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            