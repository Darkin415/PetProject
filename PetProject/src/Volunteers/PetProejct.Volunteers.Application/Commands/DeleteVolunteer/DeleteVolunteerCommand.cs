
using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.DeleteVolunteer;

public record DeleteVolunteerCommand(

    Guid VolunteerId
) :ICommand;
