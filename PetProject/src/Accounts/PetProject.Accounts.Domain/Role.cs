using Microsoft.AspNetCore.Identity;

namespace PetProject.Accounts.Domain;

public class Role : IdentityRole<Guid> 
{
    public List<RolePermission> RolePermissions { get; set; } 
}