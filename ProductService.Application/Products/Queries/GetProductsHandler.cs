using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Products.Queries;

public class GetProductsHandler(IProductQueryService query) : IRequestHandler<GetProductsCommand, PagedResult<List<ProductDTO>>>
{
    public async Task<PagedResult<List<ProductDTO>>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await query.GetAllExistingProductsAsync(
            request.SortOrder,
            request.SortColumn,
            request.PageSize,
            request.PageNumber,
            cancellationToken);

        return PagedResult<List<ProductDTO>>.Create(
            products.Values, 
            products.PageSize, 
            products.PageNumber, 
            products.TotalCount);
    }
}