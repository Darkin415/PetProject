using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Exctensions;
using PetProject.API.Module;
using PetProject.API.Response;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static PetProject.API.Response.Envelope;

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
    [HttpPut("{id:guid}/full-name")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromServices] IValidator<AddUpdateMainInfoCommand> validator,
        [FromBody] AddUpdateMainInfoCommand command,
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
}