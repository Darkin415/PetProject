
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
