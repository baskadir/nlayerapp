using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.DbContexts;

namespace NLayerApp.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsWithCategoryAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync(); // Eager loading
    }
}
