namespace VendomaticApi.Domain.Inventorys.Dtos;

public sealed class InventoryDto
{
    public Guid Id { get; set; }
    public int? InventoryId { get; set; }
    public int VendingMachineId { get; set; }
    public int ProductId { get; set; }
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
