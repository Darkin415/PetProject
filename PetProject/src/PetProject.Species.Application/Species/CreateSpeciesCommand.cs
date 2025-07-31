namespace PetProject.Species.Application.Species;

public record CreateSpeciesCommand(string Title) : ICommand;