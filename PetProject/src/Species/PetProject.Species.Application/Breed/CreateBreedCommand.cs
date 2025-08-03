
using PetProject.Core.Abstraction;

namespace PetProject.Species.Application.Breed;

public record CreateBreedCommand(string Title, Guid SpeciesId) : ICommand;
