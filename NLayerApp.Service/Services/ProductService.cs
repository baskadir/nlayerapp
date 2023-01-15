using AutoMapper;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;

namespace NLayerApp.Service.Services;

public class ProductService : Service<Product>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
    {
        List<Product> products = await _productRepository.GetProductsWithCategoryAsync();
        List<ProductWithCategoryDto> productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
    }
}
