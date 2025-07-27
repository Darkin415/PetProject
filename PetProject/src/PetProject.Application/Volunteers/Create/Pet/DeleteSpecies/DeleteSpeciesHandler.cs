using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Pet.DeleteSpecies;

public class DeleteSpeciesHandler 
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        ILogger<DeleteSpeciesHandler> logger,
        IUnitOfWork unitOfWork,
        IReadDbContext readDbContext)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken cancellationToken)
    {
        var speciesIdResult = SpeciesId.Create(command.SpeciesId);

        var speciesId = speciesIdResult.Value;

        var speciesResult = await _speciesRepository.GetSpeciesByIdAsync(speciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedExists = await _readDbContext.Pets.AnyAsync(p => p.SpeciesId == speciesId.Value);
        if (breedExists)
            return Error.Conflict("breed.exsists",
                "It is not possible to remove the species, because the animal has it.").ToErrorList();
        
        _speciesRepository.DeleteSpecies(speciesResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Species deleted with id {speciesId}", speciesId);

        return speciesResult.Value.Id.Value;
    }
}