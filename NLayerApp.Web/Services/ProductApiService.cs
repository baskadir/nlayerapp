using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Products;

namespace NLayerApp.Web.Services;

public class ProductApiService
{
	private readonly HttpClient _httpClient;

	public ProductApiService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
	{
		var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");
		return response.Data;
	}

	public async Task<ProductDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");
		return response.Data;
	}

	public async Task<ProductAddDto> SaveAsync(ProductAddDto productAddDto)
	{
		var response = await _httpClient.PostAsJsonAsync("products", productAddDto);

		if (!response.IsSuccessStatusCode) return null;

		var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductAddDto>>();

		return responseBody.Data;
	}

	public async Task<bool> UpdateAsync(ProductDto productDto)
	{
		var response = await _httpClient.PutAsJsonAsync("products", productDto);

		return response.IsSuccessStatusCode;
	}

	public async Task<bool> RemoveAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"products/{id}");

		return response.IsSuccessStatusCode;
	}
}
