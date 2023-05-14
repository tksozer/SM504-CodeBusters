namespace VendomaticApi.Domain.MachineOperators.Dtos;

public sealed class MachineOperatorForCreationDto
{
    public Guid CorrelationId { get; set; }
    public string Name { get; set; }
}
