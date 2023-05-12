namespace VendomaticApi.Domain.Products.Models;

public sealed class ProductForUpdate
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
