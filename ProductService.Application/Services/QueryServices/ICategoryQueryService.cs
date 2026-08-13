using ProductService.Application.Categories;
using ProductService.Application.Common;
using ProductService.Domain.Models;

namespace ProductService.Application.Services.QueryServices;

public interface ICategoryQueryService
{
    Task<CategoryDTO> GetExistingCategoryAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<List<CategoryDTO>>> GetAllExistingCategoriesAsync(
        string sortOrder,
        string? sortColumn,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default);
}