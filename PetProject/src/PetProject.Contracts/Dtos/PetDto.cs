namespace PetProject.Contracts.Dtos;

public class PetDto
{

    public Guid Id { get; init; }

    public Guid VolunteerId { get; init; }

    public string Nickname { get; init; } = default!;

    public string Photos { get; init; } = string.Empty;

    public int Position { get; init; } 

    public string PetInfo { get; init; } = string.Empty;

    public string Color { get; init; } = default!;

    public string StatusHealth { get; init; } = default!;

    public int Attributes { get; init; }

    public int OwnerTelephoneNumber { get; init; } = default!;

    public string CastrationStatus { get; init; } = default!;

    public string VaccinationStatus { get; init; } = default!;

}
