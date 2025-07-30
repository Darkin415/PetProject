using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetProject.Application.Abstraction;
using PetProject.Application.Authorization.DataModels;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Authorization.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(UserManager<User> userManager, ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = command.Email,
            UserName = command.UserName,
            
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            
            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

            return new ErrorList(errors);
            
        }
        
        _logger.LogInformation("User created: {userName} a new account with password.", command.UserName);
        
        return Result.Success<ErrorList>();
    }
}
