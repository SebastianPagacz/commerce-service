using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Products.Commands;

public class AssignCategoryHandler(
    IRepository<Product> productRepository, 
    ICategoryQueryService categoryQuery,
    IUnitOfWork uow) : IRequestHandler<AssignCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AssignCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await categoryQuery.GetExistingCategoryAsync(request.CategoryId, cancellationToken);
        if (existingCategory is null)
            return Result<Guid>.Fail($"Category with Id {request.CategoryId} was not found.");

        var existingProduct = await productRepository.GetAsync(request.ProductId, cancellationToken);
        if (existingProduct is null || existingProduct.IsDeleted)
            return Result<Guid>.Fail($"Product with Id {request.ProductId} was not found.");

        existingProduct.AssignCategory(request.CategoryId);
        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(existingProduct.Id);
    }
}