namespace PetProejct.Volunteers.Application.Commands;

public record RemovePetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<string> PhotoNames);