namespace PetProject.Accounts.Controllers.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password);