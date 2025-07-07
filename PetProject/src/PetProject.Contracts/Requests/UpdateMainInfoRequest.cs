using PetProject.Contracts.Commands;
using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Contracts.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, string TelephonNumber, string Description)
{
    public UpdateMainInfoCommand ToCommand(Guid id) => new(id, FullName, TelephonNumber, Description);
}
