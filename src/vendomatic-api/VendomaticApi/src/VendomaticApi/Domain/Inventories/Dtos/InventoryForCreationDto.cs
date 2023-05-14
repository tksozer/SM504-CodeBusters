namespace VendomaticApi.Domain.Inventories.Dtos;

public sealed class InventoryForCreationDto
{
    public int IsleNumber { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
