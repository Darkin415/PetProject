using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;

using PetProject.Core.Abstraction;
using PetProject.Core.Database;
using PetProject.Core.Enum;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Contracts;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Application.Species;

public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSpeciesHandler(ISpeciesRepository speciesRepository, 
        [FromKeyedServices(ModuleKey.Species)] IUnitOfWork unitOfWork)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken)
    {
        var name = Title.Create(command.Title);
        if (name.IsFailure)
            return name.Error.ToErrorList();
        
        var speciesId = SpeciesId.NewSpeciesId();
        
        var species = new Domain.PetSpecies.Species(speciesId, name.Value);

        var speciesResult = await _speciesRepository.AddSpecies(species, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error;
        

        return speciesResult.Value;

    }
}