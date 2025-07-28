namespace PetProject.Application.Authorizations.DataModels;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}