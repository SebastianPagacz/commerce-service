using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Products.Commands;
using ProductService.Application.Products.Queries;

namespace ProductService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return CreatedAtAction("GetById", new {id = result.Value}, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccess)
            return Ok($"api/incidents/{result.Value}");

        return NotFound(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(result.Message);
    }
}