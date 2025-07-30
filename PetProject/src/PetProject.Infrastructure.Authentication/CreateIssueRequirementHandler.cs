using Microsoft.AspNetCore.Authorization;

namespace PetProject.Infrastructure.Authentication;

public class CreateIssueRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permission = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
        if (permission == null)
            return;

        if (permission.Value == requirement.Code)
        {
            context.Succeed(requirement);
        }
       
    }
}