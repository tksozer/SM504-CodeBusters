namespace VendomaticApi.Domain.MachineOperators.Dtos;

using SharedKernel.Dtos;

public sealed class MachineOperatorParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
