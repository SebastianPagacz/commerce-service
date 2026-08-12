using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Products.Commands;

public class DeleteProductHandler(IRepository<Product> repository, IUnitOfWork uow) : IRequestHandler<DeleteProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetAsync(request.Id, cancellationToken);

        if (existingProduct is null || existingProduct.IsDeleted)
            return Result<string>.Fail($"Product with Id {request.Id} was not found.");

        existingProduct.Delete();

        repository.Add(existingProduct);
        await uow.CommitAsync(cancellationToken);

        return Result<string>.Success($"Product with Id {request.Id} has been deleted.");
    }
}