using ProductService.Application.Common;
using ProductService.Application.Products;

namespace ProductService.Application.Services.QueryServices;

public interface IProductQueryService
{
    Task<ProductDTO> GetExistingProductAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<List<ProductDTO>>> GetAllExistingProductsAsync(
        string? sortOrder,
        string? sortColumn,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default);
    // Task<PagedResult<List<ProductDTO>>> GetExistingProductWithoutCategoriesAsync(CancellationToken cancellationToken = default);
}