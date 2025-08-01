using PetProject.Contracts.Dtos;
using PetProject.Core.Abstraction;

namespace PetProejct.Volunteers.Application.Commands.AddPetPhotos;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId) : ICommand;
