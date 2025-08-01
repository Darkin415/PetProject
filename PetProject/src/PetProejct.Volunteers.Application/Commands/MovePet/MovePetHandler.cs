using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProject.Contracts;
using PetProject.Contracts.Ids;
using PetProject.Contracts.ValueObjects;
using PetProject.Core.Database;
using PetProject.SharedKernel;

namespace PetProejct.Volunteers.Application.Commands.MovePet;

public class MovePetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MovePetHandler> _logger;

    public MovePetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<MovePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(MovePetCommand command, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId);

            var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId.Value, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petId = PetId.Create(command.PetId);

            var petResult = _volunteersRepository.GetByPetId(petId.Value);
            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            var newPositionResult = Position.Create(command.NewPosition);
            if (newPositionResult.IsFailure)
                return newPositionResult.Error.ToErrorList();

            var petToMoveResult = volunteerResult.Value.MovePet(petResult.Value, newPositionResult.Value);
            if (petToMoveResult.IsFailure)
                return petToMoveResult.Error.ToErrorList();

            await _unitOfWork.SaveChanges(cancellationToken);
            
            transaction.Commit();
            
            _logger.LogInformation("Move pet {id} position.", command.PetId);

            return petResult.Value.Id.Value;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            
            _logger.LogError(ex, "Error move pet {id} position", command.PetId);
            
            return Error.Failure("failure_move_pet", "Error move pet position").ToErrorList();
        }
        
    }
}