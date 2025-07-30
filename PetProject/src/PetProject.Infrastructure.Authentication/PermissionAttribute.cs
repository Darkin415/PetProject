using Microsoft.AspNetCore.Authorization;

namespace PetProject.Infrastructure.Authentication;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement  
{
    

    public PermissionAttribute(string code) 
        : base(policy: code)
    {
        Code = code;
    }
    
    public string Code { get; set; }
}