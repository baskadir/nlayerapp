using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Filters;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Categories;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;
using NLayerApp.Service.Services;

namespace NLayerApp.API.Controllers;

public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Category> categories = (await _categoryService.GetAllAsync()).ToList();
        List<CategoryDto> categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
        return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
    }

    [ServiceFilter(typeof(NotFoundFilter<Category>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        Category category = await _categoryService.GetByIdAsync(id);
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
    }

    [HttpGet("[action]/{categoryId}")]
    public async Task<IActionResult> GetCategoryByIdWithProducts(int categoryId)
    {
        return CreateActionResult(await _categoryService.GetCategoryByIdWithProductsAsync(categoryId));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
    {
        Category category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryAddDto));
        CategoryDto dto = _mapper.Map<CategoryDto>(category);
        return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
    {
        await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Update(int id)
    {
        Category category = await _categoryService.GetByIdAsync(id);
        await _categoryService.RemoveAsync(category);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}
