

using PetProject.Core.Abstraction;

namespace PetProject.Volunteers.Contracts.DTOs;

public class VolunteerDto : IQuery
{
    public Guid Id { get; init; }

    public string Description { get; init; } = string.Empty;   
}



public class PetDto
{
    public Guid Id { get; init; }
}
