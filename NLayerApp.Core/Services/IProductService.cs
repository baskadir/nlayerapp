using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;

namespace NLayerApp.Core.Services;

public interface IProductService : IService<Product>
{
    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync();
}
