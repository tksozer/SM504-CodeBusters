namespace VendomaticApi.Domain.Inventories.Dtos;

public sealed class InventoryDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? VendingMachineId { get; set; }
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
