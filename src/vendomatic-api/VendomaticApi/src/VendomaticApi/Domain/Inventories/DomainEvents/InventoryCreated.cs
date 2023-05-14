namespace VendomaticApi.Domain.Inventories.DomainEvents;

public sealed class InventoryCreated : DomainEvent
{
    public Inventory Inventory { get; set; } 
}
            