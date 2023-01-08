using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayerApp.Core.DTOs;
using NLayerApp.Core.DTOs.Products;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayerApp.Caching;

public class ProductServiceCache : IProductService
{
    private const string CacheProductKey = "products_cache";
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceCache(IProductRepository productRepository, IMapper mapper, IMemoryCache memoryCache, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _unitOfWork = unitOfWork;

        if(!memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _productRepository.GetProductsWithCategoryAsync().Result);
        }
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _productRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _productRepository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entities;
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).Any(expression.Compile()));
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey)
            .FirstOrDefault(x => x.Id == id);
        if(product == null)
            throw new NotFoundException($"{typeof(Product).Name} - {id} not found");

        return Task.FromResult(product);
    }

    public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategoryAsync()
    {
        var products = _memoryCache.Get<List<Product>>(CacheProductKey);
        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
    }

    public async Task RemoveAsync(Product entity)
    {
        _productRepository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _productRepository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        _productRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey)
            .Where(expression.Compile()).AsQueryable();
    }

    private async Task CacheAllProductsAsync()
    {
        _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
    }
}
