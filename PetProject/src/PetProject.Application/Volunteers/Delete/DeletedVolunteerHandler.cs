using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Contracts.Command;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.Delete;
public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IApplicationDbContext dbContext)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
    DeleteVolunteerCommand command,

    CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)       
            return validationResult.ToErrorList();
       
        var volunteerId = new VolunteerId(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        _dbContext.Volunteers.Remove(volunteerResult.Value);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Volunteer deleted with id {volunteerId}", volunteerId);

        return volunteerResult.Value.Id.Value;
    }

}

