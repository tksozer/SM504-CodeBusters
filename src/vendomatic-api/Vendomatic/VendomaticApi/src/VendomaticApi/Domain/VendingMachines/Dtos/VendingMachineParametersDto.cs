namespace VendomaticApi.Domain.VendingMachines.Dtos;

using SharedKernel.Dtos;

public sealed class VendingMachineParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
