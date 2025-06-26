using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Contracts;
using PetProject.API.Processors;
using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.DeletePhotos;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Contracts.Command;
using PetProject.Contracts.Request;
using PetProject.Contracts.ResponseExtensions;
namespace PetProject.API.Controllers;
public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        [FromServices] IValidator<AddVolunteerCommand> validator,
        CancellationToken cancellationToken = default)
    {

        var validationResult = await validator.ValidateAsync(request.ToCommand(), cancellationToken);
    
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

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
        var command = request.ToCommand(id);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
      
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
        var command = request.ToCommmand(id);       

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
        [FromBody] DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

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
        var command = request.ToCommand(id);

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

    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photo")]
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



// передлать до конца команды как с UpdateSocialMediaList и еще раз пересмотреть с валидацией моменты чтобы лучше понять.