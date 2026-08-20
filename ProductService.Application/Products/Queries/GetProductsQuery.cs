using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Queries;

public record GetProductsQuery(
        string? SortOrder,
        string? SortColumn,
        int PageSize,
        int PageNumber) : IRequest<PagedResult<List<ProductDTO>>> { }