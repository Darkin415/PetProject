using Microsoft.AspNetCore.Authorization;

namespace PetProject.Infrastructure.Authentication;

public class PermissionRequirement: IAuthorizationRequirement
{
    public PermissionRequirement(string code) =>
        Code = code;
    
    
    
    public string Code { get; }
}