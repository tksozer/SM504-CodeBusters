namespace VendomaticApi.Domain.Operators.DomainEvents;

public sealed class OperatorUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            