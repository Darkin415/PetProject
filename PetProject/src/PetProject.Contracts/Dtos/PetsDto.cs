namespace PetProject.Application.Volunteers.Create.Pet.GetPets;

public class PetsDto
{
    public Guid Id;

    public Guid VolunteerId { get; init; }

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

