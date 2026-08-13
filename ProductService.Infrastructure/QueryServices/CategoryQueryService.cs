using Microsoft.EntityFrameworkCore;
using ProductService.Application.Categories;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.QueryServices;

public class CategoryQueryService(AppDbContext context) : ICategoryQueryService
{
    public async Task<PagedResult<List<CategoryDTO>>> GetAllExistingCategoriesAsync(
        string sortOrder,
        string? sortColumn,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> query = context.Categories
            .AsNoTracking()
            .Where(c => !c.IsDeleted);

        bool isDesc = sortOrder.ToLower().Trim() == "desc";

        query = sortColumn?.ToLower().Trim() switch
        {
            "name" => isDesc ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
            "createdat" => isDesc ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
            _ => isDesc ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id)
        };

        query = query
            .Skip((pageSize -1) * pageNumber)
            .Take(pageSize);
            
        var result = await query
            .Select(c => new CategoryDTO(c.Id, c.Name))
            .ToListAsync(cancellationToken);

        return PagedResult<List<CategoryDTO>>.Create(result, pageSize, pageNumber, result.Count());
    }

    public async Task<CategoryDTO> GetExistingCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => c.Id == id && c.IsDeleted)
            .Select(c => new CategoryDTO(c.Id, c.Name))
            .FirstOrDefaultAsync(cancellationToken);
    }
}