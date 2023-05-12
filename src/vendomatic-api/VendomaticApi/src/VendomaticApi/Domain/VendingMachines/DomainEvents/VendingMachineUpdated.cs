namespace VendomaticApi.Domain.VendingMachines.DomainEvents;

public sealed class VendingMachineUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            