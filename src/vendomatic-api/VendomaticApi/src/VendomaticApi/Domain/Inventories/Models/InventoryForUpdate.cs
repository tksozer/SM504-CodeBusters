namespace VendomaticApi.Domain.Inventories.Models;

public sealed class InventoryForUpdate
{
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
