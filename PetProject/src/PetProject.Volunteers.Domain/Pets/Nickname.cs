using CSharpFunctionalExtensions;
using PetProject.Contracts;

namespace PetProject.Volunteers.Domain.Pets;
public record NickName
{
    /// <summary>
    /// для ef core
    /// </summary>
    private NickName()
    {
        
    }
    public NickName(string nickname)
    {
        Name = nickname;
    }

    public string Name { get; }

    public static Result<NickName, Error> Create(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Errors.General.ValueIsInvalid("Nickname");

        return new NickName(nickname);
    }
}
