namespace VendomaticApi.Domain.Operators.Dtos;

using SharedKernel.Dtos;

public sealed class OperatorParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
