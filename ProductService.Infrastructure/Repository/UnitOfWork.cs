using ProductService.Domain.Abstractions;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}