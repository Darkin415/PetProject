using PetProject.Contracts.Dtos;
using PetProject.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Command;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string TelephonNumber,
    string Description);
