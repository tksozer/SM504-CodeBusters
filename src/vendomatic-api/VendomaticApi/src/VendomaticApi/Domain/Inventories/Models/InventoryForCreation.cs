namespace VendomaticApi.Domain.Inventories.Models;

public sealed class InventoryForCreation
{
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
