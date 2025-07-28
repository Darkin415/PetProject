using PetProject.Application.Commands;

namespace PetProject.Application.Volunteers.Create.Species;

public record CreateSpeciesCommand(string Title) : ICommand;