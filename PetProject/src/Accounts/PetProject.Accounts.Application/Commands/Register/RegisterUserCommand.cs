using ICommand = System.Windows.Input.ICommand;

namespace PetProject.Accounts.Application.Commands.Register;

public record RegisterUserCommand(string Email, string UserName, string Password) : Core.Abstraction.ICommand;