using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PetProject.Core.Database;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Contracts;
using PetProject.Volunteers.Contracts;

namespace PetProject.Species.Application.DeleteBreed;

public class DeleteBreedHandler 
{
    private readonly ISpeciesContract _speciesRepository;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetCheckContract _petCheckContract;

    public DeleteBreedHandler(
        ISpeciesContract speciesRepository,
        ILogger<DeleteBreedHandler> logger,
        IUnitOfWork unitOfWork,
        IPetCheckContract petCheckContract)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _petCheckContract = petCheckContract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteBreedCommand command, CancellationToken cancellationToken)
    {
        var breedIdResult = BreedId.Create(command.BreedId);

        var breedId = breedIdResult.Value;

        var breedResult = await _speciesRepository.GetBreedByIdAsync(breedId, cancellationToken);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        var breedExists = await _petCheckContract.IsBreedUsedInPetsAsync(breedId.Value, cancellationToken);
        
        
        _speciesRepository.DeleteBreed(breedResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Breed deleted with id {breedId}", breedId);

        return breedResult.Value.Id.Value;

    }
}