using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Abstraction;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Contracts.Commands;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Delete;
public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IUnitOfWork unitOfWork
        )
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        
        _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer deleted with id {volunteerId}", volunteerId);

        return volunteerResult.Value.Id.Value;
    }

}

