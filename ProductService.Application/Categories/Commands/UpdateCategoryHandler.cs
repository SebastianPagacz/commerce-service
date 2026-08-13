using FluentValidation;
using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Categories.Commands;

public class UpdateCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<UpdateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await repository.GetAsync(request.Id, cancellationToken);

        if (existingCategory is null || existingCategory.IsDeleted)
            return Result<Guid>.Fail($"Category with Id {request.Id} was not found.");

        if (request.Name.Length > 255)
            return Result<Guid>.Fail($"Category name can't be empty or exceed 255 characters.");

        existingCategory.SetName(request.Name);
        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(existingCategory.Id);
    }
}