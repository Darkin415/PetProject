using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using Microsoft.Extensions.Logging;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    UpdateMainInfoRequest Request);

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
    UpdateMainInfoCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.VolunteerId);

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

        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Updated volunteer with id {volunteerId}" , volunteerId);

        return result;
    }
}
