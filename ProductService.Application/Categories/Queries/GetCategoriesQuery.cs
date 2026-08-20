using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Categories.Queries;

public record GetCategoriesQuery(
    string? SortOrder, 
    string? SortColumn, 
    int PageSize, 
    int PageNumber) : IRequest<PagedResult<List<CategoryDTO>>>
{
    
}