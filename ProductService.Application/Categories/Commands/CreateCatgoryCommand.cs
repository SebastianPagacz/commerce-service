using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Categories.Commands;

public record CreateCategoryCommand(string Name) : IRequest<Result<Guid>>
{
    
}