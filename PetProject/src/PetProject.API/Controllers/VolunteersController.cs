using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Contracts;
using PetProject.API.Controllers.Pets.Requests;
using PetProject.API.Processors;
using PetProject.Application.Commands;
using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Application.Volunteers.Create.Pet.MovePet;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.DeletePhotos;
using PetProject.Application.Volunteers.GetVolunteers;
using PetProject.Application.Volunteers.Queries;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Contracts.Commands;
using PetProject.Contracts.Extensions;
using PetProject.Contracts.Requests;


namespace PetProject.API.Controllers;
public class VolunteersController : ApplicationController
{
    [HttpPut("pet/move-position/{volunteerId:guid}/{petId:guid}/{newPosition:int}")]

    public async Task<ActionResult> MovePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromRoute] int newPosition,
        [FromServices] MovePetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new MovePetCommand(volunteerId, petId, newPosition);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    
    
    
    [HttpGet]
    public async Task<ActionResult> GetVolunteer(
        [FromQuery] GetVolunteerWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(
        [FromRoute] Guid id,      
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.Handle(query, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }




    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        [FromServices] IValidator<AddVolunteerCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new AddVolunteerCommand(
            request.Title,
            request.LinkMedia,
            request.Information,
            request.Email,
            request.PhoneNumber,
            request.FullName,
            request.SocialMedias);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromServices] IValidator<UpdateMainInfoCommand> validator,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateMainInfoCommand(
            id, 
            request.FullName, 
            request.TelephonNumber, 
            request.Description);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    [HttpPut("{id:guid}/social-medias")]
    public async Task<ActionResult> UpdateSocialList(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialListHandler handler,
        [FromServices] IValidator<UpdateSocialNetworksCommand> validator,
        [FromBody] UpdateSocialListRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateSocialNetworksCommand(id, request.SocialMedias);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerCommand> validator,     
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromServices] AddPetHandler handler,
        [FromServices] IValidator<AddPetCommand> validator,
        [FromForm] AddPetRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddPetCommand(
            id,
            request.NickName,
            request.Breed,
            request.Species,
            request.Weight,
            request.Height,
            request.Color,
            request.StatusHealth,
            request.OwnerTelephonNumber,
            request.CastrationStatus,
            request.VaccinationStatus,
            request.BirthDate,
            request.Status,
            request.DateOfCreation);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photo")]
    public async Task<IActionResult> UploadFiles(
         [FromServices] UploadPetPhotosHandler handler,
         Guid volunteerId,
         Guid petId,
         IFormFileCollection photos,
         CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();

        var photoDtos = fileProcessor.Process(photos);

        var command = new UploadPetPhotoCommand(photoDtos, volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFiles(
       [FromServices] RemovePhotoHandler handler,
        Guid volunteerId,
        Guid petId,
       [FromQuery] IEnumerable<string> photosNames,
        CancellationToken cancellationToken)
    {
        var command = new RemovePetPhotosCommand(volunteerId, petId, photosNames);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }   
}



