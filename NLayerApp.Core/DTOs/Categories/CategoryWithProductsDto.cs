using NLayerApp.Core.DTOs.Products;

namespace NLayerApp.Core.DTOs.Categories;

public class CategoryWithProductsDto : CategoryDto
{
    public List<ProductDto>? Products { get; set; }
}
