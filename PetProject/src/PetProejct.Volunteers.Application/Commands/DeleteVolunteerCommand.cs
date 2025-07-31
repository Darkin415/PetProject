using PetProject.Contracts.Abstraction;

namespace PetProejct.Volunteers.Application.Commands;

public record DeleteVolunteerCommand(

    Guid VolunteerId
) :ICommand;
