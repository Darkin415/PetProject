using CSharpFunctionalExtensions;
using PetProject.Application.Abstraction;
using PetProject.Application.Volunteers.Create.Species;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Pet.Breed;

public class CreateBreedHandler : ICommandHandler<Guid, CreateBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;

    public CreateBreedHandler(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateBreedCommand command, CancellationToken cancellationToken)
    {
        var speciesId = SpeciesId.Create(command.SpeciesId);
        
        var name = Title.Create(command.Title);
        if (name.IsFailure)
            return name.Error.ToErrorList();
        
        var breedId = BreedId.NewBreedId();
        
        var breed = new Domain.PetSpecies.Breed(speciesId.Value, breedId, name.Value);

        var breedResult = await _speciesRepository.AddBreed(breed, cancellationToken);
        
        if (breedResult.IsFailure)
            return breedResult.Error;

        return breedResult.Value;

    }
}