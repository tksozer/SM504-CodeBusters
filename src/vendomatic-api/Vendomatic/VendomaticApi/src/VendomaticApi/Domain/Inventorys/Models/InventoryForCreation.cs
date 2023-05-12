namespace VendomaticApi.Domain.Inventorys.Models;

public sealed class InventoryForCreation
{
    public int? InventoryId { get; set; }
    public int VendingMachineId { get; set; }
    public int ProductId { get; set; }
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
