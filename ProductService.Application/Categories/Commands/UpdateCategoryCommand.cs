using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Categories.Commands;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Result<Guid>>
{
    
}