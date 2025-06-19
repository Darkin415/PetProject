using PetProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.Pet;

public record AddPetCommand(
    Guid VolunteerId, 
    IEnumerable<FileDto> Photos,
    string NickName,
    string View,
    AttributeDto PhysicalAttribute,
    string Color,
    string Breed,
    string StatusHealth,
    string OwnerTelephonNumber,
    string CastrationStatus,
    string VaccinationStatus,
    DateTime BirthDate,
    StatusHelp Status,
    DateTime DateOfCreation
    );

public record FileDto(Stream Content,string FileName, string ContentType);

public record AttributeDto(double Weight, double Height);


