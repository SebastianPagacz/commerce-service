using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Products.Queries;

public class GetProductsHandler(IProductQueryService query) : IRequestHandler<GetProductsCommand, Result<List<ProductDTO>>>
{
    public async Task<Result<List<ProductDTO>>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await query.GetAllExistingProductsAsync(cancellationToken);

        return Result<List<ProductDTO>>.Success(products);
    }
}