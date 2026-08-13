using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Categories.Queries;

public class GetCategoryByIdHandler(ICategoryQueryService queryService) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await queryService.GetExistingCategoryAsync(request.Id, cancellationToken);

        if (queryResult is null)
            return Result<CategoryDTO>.Fail($"Category with Id {request.Id} was not found.");

        return Result<CategoryDTO>.Success(queryResult);
    }
}