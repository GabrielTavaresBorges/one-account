using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreateUser = OneAccount.Application.UseCases.Users.Commands.Create;
using UpdateUser = OneAccount.Application.UseCases.Users.Commands.Update;

namespace OneAccount.Presentation.Server.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return UnprocessableEntity(new
            {
                identifier = result.Error.Identifier,
                message = result.Error.Message
            });
        }

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Value.Id },
            value: result.Value
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUser.Command command, CancellationToken cancellationToken)
    {
        // garante que o Id vem do route (evita cliente mandar id diferente no body)
        command = command with { Id = id };

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            // se você tiver um erro específico "NOT_FOUND", trate como 404
            if (result.Error.Identifier == "USER_NOT_FOUND")
                return NotFound(new { identifier = result.Error.Identifier, message = result.Error.Message });

            return UnprocessableEntity(new
            {
                identifier = result.Error.Identifier,
                message = result.Error.Message
            });
        }

        // Pode ser Ok(result.Value) (200 com body) ou NoContent() (204 sem body).
        return Ok(result.Value);
    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id) => Ok();
}
