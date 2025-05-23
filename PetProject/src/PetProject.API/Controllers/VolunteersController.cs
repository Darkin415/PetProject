using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Exctensions;
using PetProject.API.Module;
using PetProject.API.Response;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.ValueObject;
using System.Linq;
using static PetProject.API.Response.Envelope;

namespace PetProject.API.Controllers;
public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] AddVolunteerCommand command,
        CancellationToken cancellationToken = default) 
    {

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return CreatedAtAction("", result.Value);
    }
}