namespace VendomaticApi.Domain.Operators.DomainEvents;

public sealed class OperatorCreated : DomainEvent
{
    public Operator Operator { get; set; } 
}
            