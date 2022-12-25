namespace NLayerApp.Core.DTOs.Products;

public class ProductAddDto
{
    public string? Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
