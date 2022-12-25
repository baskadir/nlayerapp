using AutoMapper;
using NLayerApp.Core.DTOs.Categories;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;

namespace NLayerApp.Service.Mapping;

public class MapProfile : Profile
{
	public MapProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<Category, CategoryDto>().ReverseMap();
		CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
		CreateMap<ProductUpdateDto, Product>();
		CreateMap<ProductAddDto, Product>();
		CreateMap<Product, ProductWithCategoryDto>();
		CreateMap<Category, CategoryWithProductsDto>();
		CreateMap<CategoryUpdateDto, Category>();
		CreateMap<CategoryAddDto, Category>();
	}
}
