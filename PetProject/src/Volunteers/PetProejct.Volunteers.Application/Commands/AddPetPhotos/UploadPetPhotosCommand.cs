using PetProject.Core.Abstraction;
using PetProject.Core.DTOs;
using PetProject.Files.Contracts.Dtos;

namespace PetProejct.Volunteers.Application.Commands.AddPetPhotos;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId) : ICommand;
