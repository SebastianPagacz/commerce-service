using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Categories.Commands;
using ProductService.Application.Categories.Queries;

namespace ProductService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (result.IsSuccess)
            return CreatedAtAction("GetById", new {id = result.Value}, result.Value);

        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, string name, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateCategoryCommand(id, name), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Message);
    }
}