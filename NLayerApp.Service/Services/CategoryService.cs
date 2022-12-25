using AutoMapper;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Categories;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;

namespace NLayerApp.Service.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<CategoryWithProductsDto>> GetCategoryByIdWithProductsAsync(int categoryId)
    {
        Category category = await _categoryRepository.GetCategoryByIdWithProductsAsync(categoryId);
        CategoryWithProductsDto categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
        return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
    }
}
