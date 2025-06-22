using PetProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.Pet;

public record AddPetCommand(
    Guid VolunteerId, 
    IEnumerable<CreateFileDto> Photos,
    string NickName,
    string Breed,
    string Species,
    AttributeDto PhysicalAttribute,
    string Color,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation
    );

public record FileDto(Stream Content,string FileName, string ContentType);

public record CreateFileDto(Stream Content, string FileName);

public record AttributeDto(double Weight, double Height);



