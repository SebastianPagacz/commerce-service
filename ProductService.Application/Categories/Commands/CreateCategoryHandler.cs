using FluentValidation;
using MediatR;
using Microsoft.Extensions.Validation;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Categories.Commands;

public class CreateCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = Category.Create(request.Name);

        repository.Add(newCategory);
        await uow.CommitAsync(cancellationToken);

        return Result<Guid>.Success(newCategory.Id);
    }
}