namespace VendomaticApi.Domain.Inventories.Dtos;

using SharedKernel.Dtos;

public sealed class InventoryParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
