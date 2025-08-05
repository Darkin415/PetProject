using Microsoft.AspNetCore.Mvc;
using PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetById;
using PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetsWIthPagination;
using PetProject.Core.Abstraction;
using PetProject.Framework.Responses;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Contracts;
using PetProject.Species.Contracts.Requests;
using PetProject.Volunteers.Presentation.Pets.Requests;

namespace PetProject.Volunteers.Presentation.Pets;

public class PetsController : ApplicationController
{
    
    [HttpGet("breed/{speciesId:guid}")]
    public async Task<IActionResult> GetBreedBySpeciesId(
        [FromRoute] Guid speciesId,
        [FromServices] ISpeciesContract speciesContract,
        CancellationToken cancellationToken)
    {
        var speciesIdResult = SpeciesId.Create(speciesId);
        if (speciesIdResult.IsFailure)
        {
            return BadRequest(speciesIdResult.Error);
        }
        
        var result = await  speciesContract.GetBreedsBySpeciesId(speciesIdResult.Value.Value, cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok(result.Value); 
        }
        else
        {
            return BadRequest(result.Error); 
        }
    }
    
    [HttpGet("species")]
    public async Task<ActionResult> GetSpecies(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] ISpeciesContract speciesContract,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var response = await speciesContract.GetSpeciesWithPaginationAsync(
            query.Page, query.PageSize, cancellationToken);
        
        return Ok(response);
    }
    
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
