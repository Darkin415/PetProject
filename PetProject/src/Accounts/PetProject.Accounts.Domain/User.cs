using Microsoft.AspNetCore.Identity;

namespace PetProject.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
}