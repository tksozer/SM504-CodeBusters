namespace VendomaticApi.Domain.VendingMachines.DomainEvents;

public sealed class VendingMachineCreated : DomainEvent
{
    public VendingMachine VendingMachine { get; set; } 
}
            