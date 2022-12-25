namespace NLayerApp.Core.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; } // navigation property
    public ProductFeature? ProductFeature { get; set; } // navigation property
}
