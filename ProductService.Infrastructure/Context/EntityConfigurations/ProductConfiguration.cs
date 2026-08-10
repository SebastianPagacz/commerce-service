using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Models;

namespace ProductService.Infrastructure.Context.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Metadata.FindNavigation(nameof(Product.Categories))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(p => p.Categories)
            .WithOne()
            .HasForeignKey(pc => pc.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}