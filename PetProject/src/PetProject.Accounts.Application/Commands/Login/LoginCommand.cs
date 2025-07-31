using PetProject.Contracts.Abstraction;


namespace PetProject.Accounts.Application.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand;