using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PetProject.Core.Abstraction;
using PetProject.Framework.Responses;
using PetProject.Species.Application.Breed;
using PetProject.Species.Application.DeleteBreed;
using PetProject.Species.Application.DeleteSpecies;
using PetProject.Species.Application.Species;

namespace PetProject.Species.Presentation;

public class SpeciesController : ApplicationController
{
        

    [HttpPost("species")]
    
    
    public async Task<ActionResult> CreateSpecies(
        [FromQuery] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateSpeciesCommand(request.Title);
    
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
    
        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPost("species/{speciesId:guid}/breeds")]
    
    public async Task<ActionResult> CreateBreed(
        [FromRoute] Guid speciesId,
        [FromQuery] CreateBreedRequest request,
        [FromServices] CreateBreedHandler handler,
        CancellationToken cancellationToken)
    {
        
        var command = new CreateBreedCommand(request.Title, speciesId);
    
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
    
        return Ok(result.Value);
    }
    
    [HttpDelete("species/{speciesId:guid}")]
    public async Task<ActionResult> DeleteSpecies(
        [FromRoute] Guid speciesId,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteSpeciesCommand(speciesId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    
    [HttpDelete("breed/{breedId:guid}")]
    public async Task<ActionResult> DeleteBreed(
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteBreedCommand(breedId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }



}