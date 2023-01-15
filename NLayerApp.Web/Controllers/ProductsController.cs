using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerApp.Core.DTOs.Categories;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;
using NLayerApp.Service.Services;
using NLayerApp.Web.Filters;

namespace NLayerApp.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View((await _productService.GetProductsWithCategoryAsync()).Data);
    }

	public async Task<IActionResult> Save()
	{
		var categories = await _categoryService.GetAllAsync();
		var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

		ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");

		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Save(ProductAddDto productAddDto)
	{
		if (ModelState.IsValid)
		{
			await _productService.AddAsync(_mapper.Map<Product>(productAddDto));
			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryService.GetAllAsync();
		var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

		ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");

		return View();
	}

	[ServiceFilter(typeof(NotFoundFilter<Product>))]
	public async Task<IActionResult> Update(int id)
	{
		var product = await _productService.GetByIdAsync(id);

		var categories = await _categoryService.GetAllAsync();
		var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
		ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

		return View(_mapper.Map<ProductUpdateDto>(product));
	}

	[HttpPost]
	public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
	{
		if (ModelState.IsValid)
		{
			await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryService.GetAllAsync();
		var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
		ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name", productUpdateDto.CategoryId);

		return View(productUpdateDto);
	}

	public async Task<IActionResult> Remove(int id)
	{
		var product = await _productService.GetByIdAsync(id);

		await _productService.RemoveAsync(product);
		return RedirectToAction(nameof(Index));
	}
}
