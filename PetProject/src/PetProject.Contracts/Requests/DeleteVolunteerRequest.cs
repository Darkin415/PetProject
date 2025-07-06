using PetProject.Contracts.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Request;

public record DeleteVolunteerRequest()
{
    public DeleteVolunteerCommand ToCommand(Guid id) => new(id);
}
