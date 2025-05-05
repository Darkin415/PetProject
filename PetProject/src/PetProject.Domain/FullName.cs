using PetProject.Domain.Shared;
namespace PetProject.Domain;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Surname { get; }
    private FullName(string firstName, string lastName, string surname)
    {
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
    }
    public static Result<FullName> Create(string firstName, string lastName, string surname)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return "Firstname can not be empty";

        if (string.IsNullOrWhiteSpace(lastName))
            return "Lastname can not be empty";

        if (string.IsNullOrWhiteSpace(surname))
            return "Surname can not be empty";

        return new FullName(firstName, lastName, surname);
    }
}

