namespace VendomaticApi.Domain.VendingMachines.Dtos;

public sealed class VendingMachineForUpdateDto
{
    public string Alias { get; set; }
    public int RatingCount { get; set; }
    public int Rating { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string MachineType { get; set; }
    public int TotalIsleNumber { get; set; }
    public string Status { get; set; }
    public Guid? MachineOperatorId { get; set; }
}
