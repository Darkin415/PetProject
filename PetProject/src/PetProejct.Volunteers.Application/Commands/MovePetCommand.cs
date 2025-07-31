namespace PetProejct.Volunteers.Application.Commands;

public record MovePetCommand(Guid VolunteerId, Guid PetId, int NewPosition);
