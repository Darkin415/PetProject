namespace PetProject.Volunteers.Contracts.DTOs;

public class PetsDto
{
    public Guid Id;

    public Guid BreedId { get; init; }
    
    public Guid SpeciesId { get; init; }
    
    public Guid VolunteerId { get; init; }
    
    public double? Weight { get; private set; }
    
    public double? Height { get; private set; }

    public string? Nickname { get; private set; } = null;

    public string? Color { get; private set; } = null;

    public string? CastrationStatus { get; private set; } = null;

    public string? VaccinationStatus { get; private set; } = null;   

    public PetFileDto[] Photos { get; private set; } = null!;

    public class PetFileDto
    {
        public string PathToStorage { get; set; } = string.Empty;

        public int Size { get; set; }
    }

}

