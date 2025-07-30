using Microsoft.AspNetCore.Identity;

namespace PetProject.Application.Authorization.DataModels;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } 
}