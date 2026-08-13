using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDTO>> { }