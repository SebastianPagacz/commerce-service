using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Products.Commands;

public class CreateProductHandler(IRepository<Product> repo, IUnitOfWork uow) : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(
            request.Name,
            request.Description, 
            request.Price, 
            request.Stock);

        repo.Add(newProduct);
        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(newProduct.Id);
    }
}