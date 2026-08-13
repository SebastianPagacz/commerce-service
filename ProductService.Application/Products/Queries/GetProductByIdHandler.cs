using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Products.Queries;

public record GetProductByIdHandler(IProductQueryService query) : IRequestHandler<GetProductByIdQuery, Result<ProductDTO>>
{
    public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var exisitingProduct = await query.GetExistingProductAsync(request.Id, cancellationToken);

        if (exisitingProduct is null)
            return  Result<ProductDTO>.Fail("Product was not found");

        return Result<ProductDTO>.Success(exisitingProduct);
    }
}