using PetProject.Contracts.Dtos;

namespace PetProject.Application.Commands;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId) : ICommand;
