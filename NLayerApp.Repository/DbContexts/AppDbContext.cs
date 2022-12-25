using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities;
using System.Reflection;

namespace NLayerApp.Repository.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories{ get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
