using CSharpFunctionalExtensions;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using PetProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProject.API.Module;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using static PetProject.Domain.Shared.ValueObject.Errors;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public record AddUpdateMainInfoCommand(

    UpdateMainInfoRequest Request
);

public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Handle(
        AddUpdateMainInfoCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.Request.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var telephonNumber = TelephonNumber.Create(command.Request.TelephonNumber).Value;

        var description = Description.Create(command.Request.Description).Value;

        var fullNameResult = FullName.Create(
            command.Request.FullName.FirstName,
            command.Request.FullName.LastName,
            command.Request.FullName.Surname).Value;


        volunteerResult.Value.UpdateMainInfo(fullNameResult, description, telephonNumber);

        var result = await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);

        return result;
    }
}
