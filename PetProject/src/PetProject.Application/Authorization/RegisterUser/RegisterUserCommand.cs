using PetProject.Application.Commands;

namespace PetProject.Application.Authorization.RegisterUser;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;