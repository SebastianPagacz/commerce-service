using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Commands;

public record UpdateProductCommand(
    Guid Id, 
    string? Name, 
    string? Description, 
    decimal? Price, 
    int? Stock) : IRequest<Result<Guid>>
{
    
}