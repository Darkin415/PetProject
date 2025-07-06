using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Command;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId);
