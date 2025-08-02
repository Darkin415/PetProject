using Microsoft.AspNetCore.Authorization;
using PetProject.Accounts.Application.Authorization;

namespace PetProject.Accounts.Application;

public class CreateIssueRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permission)
    {
        var userPermission = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
        if (userPermission == null)
            return;

        if (userPermission.Value == permission.Code)
        {
            context.Succeed(permission);
        }
       
    }
}