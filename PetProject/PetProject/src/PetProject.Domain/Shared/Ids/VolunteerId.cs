using CSharpFunctionalExtensions;

namespace PetProject.Domain.Shared.Ids;
public record VolunteerId
{
    public Guid Value { get; }
    public VolunteerId(Guid value)
    {
        Value = value;
    }
    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
    public static VolunteerId Empty() => new(Guid.Empty);

    public static implicit operator Guid(VolunteerId volunterId)
    {
        ArgumentNullException.ThrowIfNull(volunterId);
        return volunterId.Value;
    }
    public static Result<VolunteerId, string> Create(Guid value)
    {
        if (value == Guid.Empty)
            return "Id cannot be empty";
        return new VolunteerId(value);
    }
}

