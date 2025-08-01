using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.Core.Abstraction;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Contracts;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Application.Species;

public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
{
    private readonly ISpeciesContract _speciesContract;

    public CreateSpeciesHandler(ISpeciesContract speciesContract)
    {
        _speciesContract = speciesContract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken)
    {
        var name = Title.Create(command.Title);
        if (name.IsFailure)
            return name.Error.ToErrorList();
        
        var speciesId = SpeciesId.NewSpeciesId();
        
        var species = new Domain.PetSpecies.Species(speciesId, name.Value);

        var speciesResult = await _speciesContract.AddSpecies(species, cancellationToken);
        

        return species.Id.Value;

    }
}