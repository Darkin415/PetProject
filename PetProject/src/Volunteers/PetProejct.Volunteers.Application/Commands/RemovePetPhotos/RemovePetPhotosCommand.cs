namespace PetProejct.Volunteers.Application.Commands.RemovePetPhotos;

public record RemovePetPhotosCommand(Guid VolunteerId, Guid PetId, IEnumerable<string> PhotoNames);