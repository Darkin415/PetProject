using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetProject.Application.Abstraction;
using PetProject.Application.Database;
using PetProject.Application.Models;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Contracts.Dtos;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Pet.DeleteBreed;

public class DeleteBreedHandler 
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;

    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        ILogger<DeleteBreedHandler> logger,
        IUnitOfWork unitOfWork,
        IReadDbContext readDbContext)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeleteBreedCommand command, CancellationToken cancellationToken)
    {
        var breedIdResult = BreedId.Create(command.BreedId);

        var breedId = breedIdResult.Value;

        var breedResult = await _speciesRepository.GetBreedByIdAsync(breedId, cancellationToken);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        var breedExists = await _readDbContext.Pets.AnyAsync(p => p.BreedId == breedId.Value);
        if (breedExists)
            return Error.Conflict("breed.exsists",
                "It is not possible to remove the breed, because the animal has it.").ToErrorList();
        
        _speciesRepository.DeleteBreed(breedResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Breed deleted with id {breedId}", breedId);

        return breedResult.Value.Id.Value;

    }
}