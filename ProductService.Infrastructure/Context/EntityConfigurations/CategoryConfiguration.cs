using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Models;

namespace ProductService.Infrastructure.Context.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany<ProductCategory>()
            .WithOne()
            .HasForeignKey(pc => pc.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}