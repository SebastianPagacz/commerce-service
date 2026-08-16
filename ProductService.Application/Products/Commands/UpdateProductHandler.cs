using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Products.Commands;

public class UpdateProductHandler(
    IRepository<Product> repository, 
    IUnitOfWork uow) : IRequestHandler<UpdateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetAsync(request.Id, cancellationToken);

        if (existingProduct is null || existingProduct.IsDeleted)
            return Result<Guid>.Fail($"Product with Id {request.Id} was not found.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            existingProduct.SetName(request.Name);

        if (!string.IsNullOrWhiteSpace(request.Description))
            existingProduct.SetDescription(request.Description);

        if (request.Price is not null)
            existingProduct.SetPrice((decimal)request.Price);

        if (request.Stock is not null)
            existingProduct.SetStock((int)request.Stock);

        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(existingProduct.Id);
    }
}