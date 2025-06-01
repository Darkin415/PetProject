using CSharpFunctionalExtensions;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using PetProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    public async Task<Result<Guid, Error>> Handle(        
        CancellationToken cancellationToken = default)
    {
        
    }
}
