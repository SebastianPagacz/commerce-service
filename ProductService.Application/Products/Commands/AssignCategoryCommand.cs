using MediatR;
using ProductService.Application.Common;

namespace ProductService.Application.Products.Commands;

public record AssignCategoryCommand(Guid ProductId, Guid CategoryId) : IRequest<Result<Guid>>;