using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Contracts.Command;
using FluentValidation;
using PetProject.Application.Extensions;

namespace PetProject.Application.Volunteers.UpdateMainInfo;
public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger,
        IApplicationDbContext dbContext,
        IValidator<UpdateMainInfoCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _dbContext = dbContext;
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

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated volunteer with id {volunteerId}" , volunteerId);

        return volunteerResult.Value.Id.Value;
    }
}
