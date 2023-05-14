namespace VendomaticApi.Domain.MachineOperators.DomainEvents;

public sealed class MachineOperatorCreated : DomainEvent
{
    public MachineOperator MachineOperator { get; set; } 
}
            