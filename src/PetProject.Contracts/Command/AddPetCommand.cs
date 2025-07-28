using PetProject.Contracts.Dtos;
using PetProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Command
{
    public record AddPetCommand(
    Guid VolunteerId,
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
    DateTime DateOfCreation);   
}
