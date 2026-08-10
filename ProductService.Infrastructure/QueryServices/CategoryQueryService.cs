using Microsoft.EntityFrameworkCore;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.QueryServices;

public class CategoryQueryService(AppDbContext context) : ICategoryQueryService
{
    public async Task<List<Category>> GetAllExistingCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => c.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category> GetExistingCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => c.Id == id && c.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }
}