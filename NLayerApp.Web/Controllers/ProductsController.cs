using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;
using NLayerApp.Web.Filters;
using NLayerApp.Web.Services;

namespace NLayerApp.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ProductApiService _productApiService;
    private readonly CategoryApiService _categoryApiService;

	public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
	{
		_productApiService = productApiService;
		_categoryApiService = categoryApiService;
	}

	public async Task<IActionResult> Index()
    {
        return View(await _productApiService.GetProductsWithCategoryAsync());
    }

	public async Task<IActionResult> Save()
	{
		var categories = await _categoryApiService.GetAllAsync();

		ViewBag.Categories = new SelectList(categories, "Id", "Name");

		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Save(ProductAddDto productAddDto)
	{
		if (ModelState.IsValid)
		{
			await _productApiService.SaveAsync(productAddDto);
			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryApiService.GetAllAsync();

		ViewBag.Categories = new SelectList(categories, "Id", "Name");

		return View();
	}

	[ServiceFilter(typeof(NotFoundFilter<Product>))]
	public async Task<IActionResult> Update(int id)
	{
		var product = await _productApiService.GetByIdAsync(id);

		var categories = await _categoryApiService.GetAllAsync();
		ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

		return View(product);
	}

	[HttpPost]
	public async Task<IActionResult> Update(ProductDto productDto)
	{
		if (ModelState.IsValid)
		{
			await _productApiService.UpdateAsync(productDto);
			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryApiService.GetAllAsync();
		ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

		return View(productDto);
	}

	public async Task<IActionResult> Remove(int id)
	{
		var product = await _productApiService.RemoveAsync(id);

		return RedirectToAction(nameof(Index));
	}
}
