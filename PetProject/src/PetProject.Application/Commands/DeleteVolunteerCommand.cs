namespace PetProject.Application.Commands;

public record DeleteVolunteerCommand(

    Guid VolunteerId
) :ICommand;
