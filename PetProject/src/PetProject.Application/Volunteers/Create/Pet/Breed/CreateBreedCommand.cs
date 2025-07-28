using PetProject.Application.Commands;

namespace PetProject.Application.Volunteers.Create.Pet.Breed;

public record CreateBreedCommand(string Title, Guid SpeciesId) : ICommand;
