using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetProject.Accounts.Domain;
using PetProject.Contracts;
using PetProject.Core.Abstraction;
using PetProject.SharedKernel;

namespace PetProject.Accounts.Application.Commands.Login;

public class LoginHandler : ICommandHandler<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<LoginHandler> _logger;
    private readonly ITokenProvider _tokenProvider;

    public LoginHandler(UserManager<User> userManager, ILogger<LoginHandler> logger, ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<string, ErrorList>> Handle(
        LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return Errors.General.NotFound().ToErrorList();
        }
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordConfirmed)
            return Errors.User.InvalidCredentials().ToErrorList();
        
        var token =  _tokenProvider.GenerateAccessToken(user);
        
        _logger.LogInformation("Successfully logged in");

        return token;

    }
}