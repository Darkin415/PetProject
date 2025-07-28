using PetProject.Application.Commands;

namespace PetProject.Application.Authorization.Login;

public record LoginCommand(string Email, string Password) : ICommand;