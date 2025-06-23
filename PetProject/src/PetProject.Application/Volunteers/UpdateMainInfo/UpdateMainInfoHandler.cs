using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Contracts.Command;

namespace PetProject.Application.Volunteers.UpdateMainInfo;
public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Handle(
    UpdateMainInfoCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var telephonNumber = TelephonNumber.Create(command.Request.TelephonNumber).Value;

        var description = Description.Create(command.Request.Description).Value;

        var fullNameResult = FullName.Create(
            command.Request.FullName.FirstName,
            command.Request.FullName.LastName,
            command.Request.FullName.Surname).Value;

        volunteerResult.Value.UpdateMainInfo(fullNameResult, description, telephonNumber);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated volunteer with id {volunteerId}" , volunteerId);

        return volunteerResult.Value.Id.Value;
    }
}
