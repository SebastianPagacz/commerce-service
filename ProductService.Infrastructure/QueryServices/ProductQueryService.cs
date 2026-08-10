using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ProductService.Application.Common;
using ProductService.Application.Products;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.QueryServices;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    // public async Task<PagedResult<List<ProductDTO>>> GetExistingProductWithoutCategoriesAsync(CancellationToken cancellationToken = default)
    // {
    //     return await context.Products
    //         .AsNoTracking()
    //         .Where(p => !p.IsDeleted)
    //         .Select(p => new ProductDTO(p.Name, p.Description, p.Price, p.Stock))
    //         .ToListAsync();
    // }

    public async Task<PagedResult<List<ProductDTO>>> GetAllExistingProductsAsync(
        string sortOrder,
        string? sortColumn,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsNoTracking();

        Expression<Func<Product, object?>> sortKey = sortColumn?.ToLower().Trim() switch
        {
            "name" => i => i.Name,
            "price" => i => i.Price,
            "stock" => i => i.Stock,
            "createdat" => i => i.CreatedAt,
            _ => i => i.Id
        };

        if(sortOrder.ToLower() == "desc")
        {
            query = query.OrderByDescending(sortKey);
        }
        else
        {
            query = query.OrderBy(sortKey);
        }
            
        query = query.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);

        var final = await query
            .Where(p => !p.IsDeleted)
            .Include(p => p.Categories)
            .Select(p => new ProductDTO(p.Name, p.Description, p.Price, p.Stock))
            .ToListAsync(cancellationToken);

        return PagedResult<List<ProductDTO>>.Create(final, pageSize, pageNumber, final.Count());
    }

    public async Task<ProductDTO> GetExistingProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.Id == id && !p.IsDeleted)
            .Select(p => new ProductDTO(p.Name, p.Description, p.Price, p.Stock))
            .FirstOrDefaultAsync(cancellationToken);
    }
}