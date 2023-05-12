namespace VendomaticApi.Domain.Products.Dtos;

public sealed class ProductForCreationDto
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
