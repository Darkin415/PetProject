using PetProject.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Command;

public record DeleteVolunteerCommand(

    Guid VolunteerId
);
