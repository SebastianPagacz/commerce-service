using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repository;

public class CategoryRepository(AppDbContext context) : IRepository<Category>
{
    public void Add(Category item)
    {
        context.Categories.Add(item);
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}