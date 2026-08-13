using FluentValidation;
using MediatR;
using Microsoft.Extensions.Validation;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Categories.Commands;

public class CreateCategoryHandler(IRepository<Category> repository, IUnitOfWork uow, IValidator<Category> validator) : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = Category.Create(request.Name);
        var validationResult = await validator.ValidateAsync(newCategory);

        if (!validationResult.IsValid)
            return Result<Guid>.Fail("Failed to create new category.");

        repository.Add(newCategory);
        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(newCategory.Id);
    }
}