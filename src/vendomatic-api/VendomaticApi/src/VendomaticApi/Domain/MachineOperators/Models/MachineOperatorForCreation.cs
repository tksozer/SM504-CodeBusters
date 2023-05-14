namespace VendomaticApi.Domain.MachineOperators.Models;

public sealed class MachineOperatorForCreation
{
    public Guid CorrelationId { get; set; }
    public string Name { get; set; }
}
