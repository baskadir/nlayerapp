using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Filters;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers;

public class ProductsController : CustomBaseController
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Product> products = (await _productService.GetAllAsync()).ToList();
        List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);
        return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
    }

    [ServiceFilter(typeof(NotFoundFilter<Product>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        Product product = await _productService.GetByIdAsync(id);
        ProductDto productDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductsWithCategory()
    {
        return CreateActionResult(await _productService.GetProductsWithCategoryAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductAddDto productAddDto)
    {
        Product product = await _productService.AddAsync(_mapper.Map<Product>(productAddDto));
        ProductDto dto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Update(int id)
    {
        Product product = await _productService.GetByIdAsync(id);
        await _productService.RemoveAsync(product);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}
