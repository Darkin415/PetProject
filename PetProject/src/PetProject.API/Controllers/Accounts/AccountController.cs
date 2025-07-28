using Microsoft.AspNetCore.Mvc;
using PetProject.API.Requests;
using PetProject.Application.AccountManagement.Commands;
using PetProject.Contracts.Extensions;

namespace PetProject.API.Controllers.Accounts;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<ActionResult> Registor(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(request.Email, request.UserName, request.Password);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}
