using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Queries;

public record GetProductByIdCommand(Guid Id) : IRequest<Result<ProductDTO>> { }