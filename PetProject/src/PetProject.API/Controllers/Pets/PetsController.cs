using Microsoft.AspNetCore.Mvc;
using PetProject.API.Controllers.Pets.Requests;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Application.Volunteers.Queries;
using PetProject.Contracts.Extensions;

namespace PetProject.API.Controllers.Pets;

public class PetsController : ApplicationController
{
    [HttpGet("pets")]
    public async Task<ActionResult> GetPets(
        [FromRoute] Guid id,
        [FromQuery] GetPetsWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery(id);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(
        [FromRoute] Guid id,      
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(id);

        var result = await handler.Handle(query, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
