using MediatR;
using ProductService.Application.Common;
using ProductService.Domain.Abstractions;
using ProductService.Domain.Models;

namespace ProductService.Application.Categories.Commands;

public class DeleteCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await repository.GetAsync(request.Id);

        if (existingCategory is null || existingCategory.IsDeleted)
            return Result<string>.Fail($"Category with Id {request.Id} was not found.");

        existingCategory.Delete();

        await uow.CommitAsync(cancellationToken);
        
        return Result<string>.Success("Category deleted.");
    }
}