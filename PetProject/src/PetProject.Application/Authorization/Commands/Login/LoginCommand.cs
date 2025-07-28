namespace PetProject.Application.AccountManagement.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand;