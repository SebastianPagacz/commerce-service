using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDTO>>
{
    
}