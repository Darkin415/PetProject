using PetProject.Application.Volunteers.Create.Pet.AddPet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;

public record UploadPetPhotoCommand(IEnumerable<CreateFileDto> Photos, Guid VolunteerId, Guid PetId);


