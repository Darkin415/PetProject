namespace PetProject.Application.AccountManagement.Commands;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;