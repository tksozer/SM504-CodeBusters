namespace VendomaticApi.Domain.Inventorys.DomainEvents;

public sealed class InventoryUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            