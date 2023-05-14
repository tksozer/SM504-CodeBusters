namespace VendomaticApi.Domain.MachineOperators.Dtos;

public sealed class MachineOperatorDto
{
    public Guid Id { get; set; }
    public Guid CorrelationId { get; set; }
    public string Name { get; set; }
}
