namespace VendomaticApi.Domain.MachineOperators.Dtos;

public sealed class MachineOperatorForUpdateDto
{
    public Guid CorrelationId { get; set; }
    public string Name { get; set; }
}
