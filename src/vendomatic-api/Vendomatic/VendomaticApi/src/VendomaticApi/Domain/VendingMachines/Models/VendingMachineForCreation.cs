namespace VendomaticApi.Domain.VendingMachines.Models;

public sealed class VendingMachineForCreation
{
    public int? VendingMachineId { get; set; }
    public string Alias { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Type { get; set; }
    public int TotalIsleNumber { get; set; }
    public string Status { get; set; }
}
