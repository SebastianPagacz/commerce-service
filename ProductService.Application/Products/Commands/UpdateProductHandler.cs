using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Products.Commands;

public class UpdateProductHandler(
    IRepository<Product> productRepository,
    IRepository<Category> categoryRepository,
    IUnitOfWork uow) : IRequestHandler<UpdateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetAsync(request.Id, cancellationToken);

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

        if (request.CategoryId is not null)
        {
            var existingCategory = await categoryRepository.GetAsync((Guid)request.CategoryId, cancellationToken);

            if (!existingCategory.IsDeleted && existingCategory is not null)
            {
                existingProduct.AssignCategory(existingCategory);
            }
        }

        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(existingProduct.Id);
    }
}