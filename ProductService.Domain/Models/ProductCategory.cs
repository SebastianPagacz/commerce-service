using ProductService.Domain.Exceptions;

namespace ProductService.Domain.Models;

public class ProductCategory
{
    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }

    private ProductCategory() { }
    private ProductCategory(Guid productId, Guid categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    internal static ProductCategory Create(Guid productId, Guid categoryId)
    {
        return new ProductCategory(productId, categoryId);
    }
}