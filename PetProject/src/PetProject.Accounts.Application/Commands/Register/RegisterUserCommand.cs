

using PetProject.Contracts.Abstraction;

namespace PetProject.Accounts.Application.Commands.Register;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;