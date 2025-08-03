namespace PetProejct.Volunteers.Application.Commands.MovePet;

public record MovePetCommand(Guid VolunteerId, Guid PetId, int NewPosition);
