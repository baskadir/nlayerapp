using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;

namespace NLayerApp.Repository.Seeds;

internal class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category { Id = 1, Name = "Teknoloji", CreatedDate = DateTime.UtcNow },
            new Category { Id = 2, Name = "Moda", CreatedDate = DateTime.UtcNow },
            new Category { Id = 3, Name = "Spor", CreatedDate = DateTime.UtcNow }
        );
    }
}
