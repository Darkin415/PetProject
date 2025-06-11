using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Domain;

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