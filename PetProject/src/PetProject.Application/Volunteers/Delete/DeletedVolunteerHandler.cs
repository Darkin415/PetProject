using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Delete;
public record DeleteVolunteerCommand(

    DeleteVolunteerRequest Request
);

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository, 
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
    DeleteVolunteerCommand command,
    CancellationToken cancellationToken = default)
    {
        var volunteerId = new VolunteerId(command.Request.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var result = await _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Volunteer deleted with id {volunteerId}", volunteerId);

        return result;
    }

}
