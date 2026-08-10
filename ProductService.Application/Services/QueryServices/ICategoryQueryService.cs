using ProductService.Domain.Models;

namespace ProductService.Application.Services.QueryServices;

public interface ICategoryQueryService
{
    Task<Category> GetExistingCategoryAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Category>> GetAllExistingCategoriesAsync(CancellationToken cancellationToken = default);
}