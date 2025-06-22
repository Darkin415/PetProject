using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.Delete;
public record DeleteVolunteerCommand(

    DeleteVolunteerRequest Request
);

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(
    DeleteVolunteerCommand command,
    CancellationToken cancellationToken = default)
    {
        var volunteerId = new VolunteerId(command.Request.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer deleted with id {volunteerId}", volunteerId);

        return volunteerResult.Value.Id.Value;
    }

}

// доделать ID-B-14 по возможности и расписать ее в Word .