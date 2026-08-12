using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Result<string>>
{
    
}