using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Contracts;
using PetProject.API.Exctensions;
using PetProject.API.Processors;
using PetProject.Application.Volunteers.Create.Pet;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Volunteers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PetProject.API.Controllers;
public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] AddVolunteerCommand command,
        [FromServices] IValidator<AddVolunteerCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

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
        var command = new UpdateMainInfoCommand(id, request);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToValidationErrorResponse();

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
        var command = new UpdateSocialNetworksCommand(id, request);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

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
        var request = new DeleteVolunteerRequest(VolunteerId: id);
        var command = new DeleteVolunteerCommand(request);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

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
        CancellationToken cancellationToken
        )
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.Photos);

        var command = new AddPetCommand(
                id,
                fileDtos,
                request.NickName,
                request.Breed,
                request.Species,
                request.Attribute,
                request.Color,           
                request.StatusHealth,
                request.OwnerTelephonNumber,
                request.CastrationStatus,
                request.VaccinationStatus,
                request.BirthDate,
                request.Status,
                request.DateOfCreation
                );

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}