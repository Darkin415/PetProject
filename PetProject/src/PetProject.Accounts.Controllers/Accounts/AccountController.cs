using Microsoft.AspNetCore.Mvc;
using PetProject.Accounts.Application.Authorization;
using PetProject.Accounts.Application.Commands.Login;
using PetProject.Accounts.Application.Commands.Register;
using PetProject.Accounts.Controllers.Requests;
using PetProject.Contracts;
using PetProject.Framework.Responses;

namespace PetProject.Accounts.Controllers.Accounts;

public class AccountController : ApplicationController
{
    [Permission("volunteer.create")]
    [HttpPost("create")]

    public IActionResult CreateVolunteer()
    {
        return Ok();
    }
    
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
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken
    )
    {
        var command = new LoginCommand(request.Email, request.Password);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}

