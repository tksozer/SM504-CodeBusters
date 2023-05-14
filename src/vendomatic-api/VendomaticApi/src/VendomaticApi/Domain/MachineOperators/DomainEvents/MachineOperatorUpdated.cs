namespace VendomaticApi.Domain.MachineOperators.DomainEvents;

public sealed class MachineOperatorUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            