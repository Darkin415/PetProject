using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetProject.API.Controllers.Pets.Requests;
using PetProject.Application.Volunteers.Create.Pet.GetBreedBySpeciesId;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Application.Volunteers.Create.Pet.GetSpecies;
using PetProject.Application.Volunteers.Queries;
using PetProject.Contracts.Extensions;
using PetProject.Domain.PetSpecies;

namespace PetProject.API.Controllers.Pets;

public class PetsController : ApplicationController
{
    
    [HttpGet("breed/{breedId:guid}")]
    public async Task<IActionResult> GetBreedBySpeciesId(
        [FromRoute] Guid breedId,
        [FromServices] GetBreedBySpeciesIdHandler handler,
        CancellationToken cancellationToken)
    {
        var speciesIdResult = SpeciesId.Create(breedId);
        if (speciesIdResult.IsFailure)
        {
            return BadRequest(speciesIdResult.Error);
        }
        
        var command = new GetBreedBySpeciesIdCommand(speciesIdResult.Value.Value);
        
        var result = await handler.Handle(command, cancellationToken);
        
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
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);
        
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
