using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using FluentValidation;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Contracts.Commands;

namespace PetProject.Application.Volunteers.UpdateMainInfo;
public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger,
        IValidator<UpdateMainInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
    UpdateMainInfoCommand command,
    CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerId = new VolunteerId(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var telephonNumber = TelephonNumber.Create(command.TelephonNumber).Value;

        var description = Description.Create(command.Description).Value;

        var fullNameResult = FullName.Create(
            command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Surname).Value;

        volunteerResult.Value.UpdateMainInfo(fullNameResult, description, telephonNumber);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated volunteer with id {volunteerId}" , volunteerId);

        return volunteerResult.Value.Id.Value;
    }
}
