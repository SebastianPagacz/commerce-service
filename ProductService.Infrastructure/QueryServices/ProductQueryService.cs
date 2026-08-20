using Microsoft.EntityFrameworkCore;
using ProductService.Application.Categories;
using ProductService.Application.Common;
using ProductService.Application.Products;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.QueryServices;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    public async Task<PagedResult<List<ProductDTO>>> GetAllExistingProductsAsync(
        string? sortOrder,
        string? sortColumn,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = context.Products.AsNoTracking().Where(p => !p.IsDeleted);

        bool isDesc = false;

        if (sortOrder is not null)
            isDesc = sortOrder.ToLower().Trim() == "desc";

        query = sortColumn?.ToLower().Trim() switch
        {
            "name" => isDesc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "price" => isDesc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
            "stock" => isDesc ? query.OrderByDescending(p => p.Stock) : query.OrderBy(p => p.Stock),
            "createdat" => isDesc ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
            _ => isDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
        };
            
        query = query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);

        var result = await query
            .Include(p => p.Categories)
            .Select(p => new ProductDTO(
                p.Id, 
                p.Name, 
                p.Description, 
                p.Price, 
                p.Stock,
                p.Categories.Select(c => new CategoryDTO(
                    c.Id, 
                    c.Name)).ToList()))
            .ToListAsync(cancellationToken);

        return PagedResult<List<ProductDTO>>.Create(result, pageSize, pageNumber, result.Count());
    }

    public async Task<ProductDTO> GetExistingProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.Id == id && !p.IsDeleted)
            .Include(p => p.Categories)
            .Select(p => new ProductDTO(
                p.Id, 
                p.Name, 
                p.Description, 
                p.Price, 
                p.Stock,
                p.Categories.Select(c => new CategoryDTO(
                    c.Id, 
                    c.Name)).ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }
}