using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PetProject.Core.Database;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Volunteers.Contracts;

namespace PetProject.Species.Application.DeleteSpecies;

public class DeleteSpeciesHandler 
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetCheckContract _petCheckContract;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        ILogger<DeleteSpeciesHandler> logger,
        IUnitOfWork unitOfWork,
        IPetCheckContract petCheckContract)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _petCheckContract = petCheckContract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken cancellationToken)
    {
        var speciesIdResult = SpeciesId.Create(command.SpeciesId);

        var speciesId = speciesIdResult.Value;

        var speciesResult = await _speciesRepository.GetSpeciesByIdAsync(speciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var speciesExists = await _petCheckContract.IsSpeciesUsedInPetsAsync(speciesId.Value, cancellationToken);
        
        _speciesRepository.DeleteSpecies(speciesResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Species deleted with id {speciesId}", speciesId);

        return speciesResult.Value.Id.Value;
    }
}