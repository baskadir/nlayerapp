using NLayerApp.Core.DTOs.Categories;

namespace NLayerApp.Core.DTOs.Products;

public class ProductWithCategoryDto : ProductDto
{
    public CategoryDto? Category { get; set; }
}
