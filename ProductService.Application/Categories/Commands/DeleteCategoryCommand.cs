using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Categories.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<Result<string>>
{
    
}