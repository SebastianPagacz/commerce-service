using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Queries;

public record GetProductsCommand() : IRequest<Result<List<ProductDTO>>> { }