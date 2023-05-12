namespace VendomaticApi.Domain.Products.Dtos;

using SharedKernel.Dtos;

public sealed class ProductParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
