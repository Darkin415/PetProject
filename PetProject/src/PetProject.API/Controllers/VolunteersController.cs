using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Exctensions;
using PetProject.API.Module;
using PetProject.API.Response;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain;
using PetProject.Domain.Shared;

namespace PetProject.API.Controllers;

[Route("[controller]")]
[ApiController]
public class VolunteersController : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
       var result = await handler.Handle(request, cancellationToken);
       
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Ok(result.Value));
       
    }
}
    
    




