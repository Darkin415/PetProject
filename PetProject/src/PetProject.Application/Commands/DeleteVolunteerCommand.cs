using PetProject.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Commands;

public record DeleteVolunteerCommand(

    Guid VolunteerId
) :ICommand;
