using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;

namespace NLayerApp.Repository.Seeds;

internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.HasData(
            new ProductFeature { Id = 1, Color = "Kırmız", Height = 100, Width = 200, ProductId = 1 },
            new ProductFeature { Id = 2, Color = "Gri", Height = 150, Width = 50, ProductId = 2 }
        );
    }
}
