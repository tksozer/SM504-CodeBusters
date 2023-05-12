namespace VendomaticApi.Domain.Inventorys.DomainEvents;

public sealed class InventoryCreated : DomainEvent
{
    public Inventory Inventory { get; set; } 
}
            