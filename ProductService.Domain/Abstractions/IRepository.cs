namespace ProductService.Domain.Abstractions;

public interface IRepository<T>
{
    void Add(T item);
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
}