using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;

namespace PetProject.Volunteers.Domain.PetValueObjects;
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
