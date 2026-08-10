using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IRepository<Product>
{
    public void Add(Product item)
    {
        context.Products.Add(item);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default) // need for pagination
    {
        return await context.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}