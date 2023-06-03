namespace VendomaticApi.Domain.VendingMachines.Dtos;

using SharedKernel.Dtos;

public sealed class FindNearestVendingMachinesParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }

    public double Latitude { get; set; }
    
    public double Longitude { get; set; } 
}