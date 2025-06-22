using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.ValueObject;

public record Email
{
    private Email(string link)
    {
        Link = link;
    }
    public string Link { get; }
    public static Result<Email, Error> Create(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("Email");
        return new Email(link);
    }
}