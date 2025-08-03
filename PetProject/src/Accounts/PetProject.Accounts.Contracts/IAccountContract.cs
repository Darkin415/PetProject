using CSharpFunctionalExtensions;
using PetProject.Accounts.Contracts.Requests;
using PetProject.SharedKernel;

namespace PetProject.Accounts.Contracts;

public interface IAccountContract
{
    Task<UnitResult<ErrorList>> RegisterUser(RegisterUserRequest request, CancellationToken cancellationToken);

    Task<Result<string, ErrorList>> LoginUser(LoginUserRequest request, CancellationToken cancellationToken);
}