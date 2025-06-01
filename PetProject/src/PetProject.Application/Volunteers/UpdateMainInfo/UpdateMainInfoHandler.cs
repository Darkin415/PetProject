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
        // дописать логику продолжить просмотр 9 видео 
        var volunteerId = new VolunteerId(command.Request.VolunteerId);
        var volunteerResult = _volunteersRepository.GetById(volunteerId, cancellationToken);


        return Guid.Empty;
    }
}
