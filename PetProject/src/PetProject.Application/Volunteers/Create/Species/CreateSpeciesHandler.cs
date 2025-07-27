using CSharpFunctionalExtensions;
using PetProject.Application.Abstraction;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Species;

public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;

    public CreateSpeciesHandler(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken)
    {
        var name = Title.Create(command.Title);
        if (name.IsFailure)
            return name.Error.ToErrorList();
        
        var speciesId = SpeciesId.NewSpeciesId();
        
        var species = new Domain.PetSpecies.Species(speciesId, name.Value);

        var speciesResult = await _speciesRepository.AddSpecies(species, cancellationToken);
        

        return species.Id.Value;

    }
}