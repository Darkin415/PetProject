using CSharpFunctionalExtensions;
using PetProject.Accounts.Application.Commands.Login;
using PetProject.Accounts.Application.Commands.Register;
using PetProject.Accounts.Contracts;
using PetProject.Accounts.Contracts.Requests;
using PetProject.SharedKernel;

namespace PetProject.Accounts.Presentation;

public class AccountsContract(RegisterUserHandler registerUserHandler, LoginHandler loginUserHandler) : IAccountContract
{
    public async Task<UnitResult<ErrorList>> RegisterUser(RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.UserName, request.Password);

        var result = await registerUserHandler.Handle(command, cancellationToken);

        return result;
    }

    public Task<Result<string, ErrorList>> LoginUser(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        
        var result = loginUserHandler.Handle(command, cancellationToken);

        return result;
    }
}