using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Commands;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId) : ICommand;
