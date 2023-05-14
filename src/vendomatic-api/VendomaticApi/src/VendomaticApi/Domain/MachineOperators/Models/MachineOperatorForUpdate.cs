namespace VendomaticApi.Domain.MachineOperators.Models;

public sealed class MachineOperatorForUpdate
{
    public Guid CorrelationId { get; set; }
    public string Name { get; set; }
}
