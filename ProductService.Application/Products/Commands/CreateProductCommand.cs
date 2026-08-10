using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    int Stock
) : IRequest<Result<Guid>> { }