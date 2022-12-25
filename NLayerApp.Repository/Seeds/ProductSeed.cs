using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;

namespace NLayerApp.Repository.Seeds;

internal class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product { Id = 1, CategoryId = 1, Price = 500, Stock = 20, CreatedDate = DateTime.UtcNow, Name = "Kulaklık" },
            new Product { Id = 2, CategoryId = 1, Price = 5000, Stock = 30, CreatedDate = DateTime.UtcNow, Name = "Telefon" },
            new Product { Id = 3, CategoryId = 1, Price = 200, Stock = 50, CreatedDate = DateTime.UtcNow, Name = "Akıllı Saat" },
            new Product { Id = 4, CategoryId = 2, Price = 400, Stock = 30, CreatedDate = DateTime.UtcNow, Name = "Gömlek" },
            new Product { Id = 5, CategoryId = 2, Price = 700, Stock = 100, CreatedDate = DateTime.UtcNow, Name = "Ayakkabı" }
        );
    }
}
