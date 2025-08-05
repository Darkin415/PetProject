using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Application.DeleteSpecies;

namespace PetProject.Species.Application.GetBreedBySpeciesId;

public class GetBreedBySpeciesIdHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    

    public GetBreedBySpeciesIdHandler(
        ISpeciesRepository speciesRepository,
        ILogger<DeleteSpeciesHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
    }
    public async Task<Result<List<Domain.PetSpecies.Breed>, ErrorList>> Handle(GetBreedBySpeciesIdCommand command, CancellationToken cancellationToken)
    {
        var speciesId = SpeciesId.Create(command.SpeciesId);

        var breedsResult = await _speciesRepository.GetBreedsBySpeciesId(speciesId.Value, cancellationToken);
        if (breedsResult.IsFailure)
            return breedsResult.Error.ToErrorList();

        return breedsResult.Value;

    }
}