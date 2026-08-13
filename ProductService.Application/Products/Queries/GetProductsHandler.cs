using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Products.Queries;

public class GetProductsHandler(IProductQueryService query) : IRequestHandler<GetProductsQuery, PagedResult<List<ProductDTO>>>
{
    public async Task<PagedResult<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await query.GetAllExistingProductsAsync(
            request.SortOrder,
            request.SortColumn,
            request.PageSize,
            request.PageNumber,
            cancellationToken);
    }
}