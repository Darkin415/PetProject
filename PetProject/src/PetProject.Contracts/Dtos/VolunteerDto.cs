using PetProject.Domain;
using PetProject.Domain.Enum;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }

    public string Description { get; init; } = string.Empty;   
}

public class PetDto
{
    public Guid Id { get; init; }
}
