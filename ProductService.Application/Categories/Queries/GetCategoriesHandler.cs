using MediatR;
using ProductService.Application.Common;
using ProductService.Application.Services.QueryServices;

namespace ProductService.Application.Categories.Queries;

public class GetCategoriesHandler(ICategoryQueryService queryService) : IRequestHandler<GetCategoriesQuery, PagedResult<List<CategoryDTO>>>
{
    public async Task<PagedResult<List<CategoryDTO>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await queryService.GetAllExistingCategoriesAsync(
            request.SortOrder, 
            request.SortColumn, 
            request.PageSize, 
            request.PageNumber,
            cancellationToken);        
    }
}