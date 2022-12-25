using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.DbContexts;

namespace NLayerApp.Repository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Category> GetCategoryByIdWithProductsAsync(int categoryId)
    {
        return await _context.Categories.Include(c => c.Products).Where(c => c.Id == categoryId).SingleOrDefaultAsync();
    }
}
