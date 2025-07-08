using PetProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Requests;

public record DeleteVolunteerRequest()
{
    public DeleteVolunteerCommand ToCommand(Guid id) => new(id);
}
