using Microsoft.AspNetCore.Mvc;
using PetProject.API.Requests;
using PetProject.Application.Authorization.Login;
using PetProject.Application.Authorization.RegisterUser;
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