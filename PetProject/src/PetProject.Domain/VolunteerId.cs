using PetProject.Domain.Shared;

namespace PetProject.Domain;
public record VolunteerId
{
    public Guid Value { get; }
    public VolunteerId(Guid value)
    {
        Value = value;
    }
    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
    public static VolunteerId Empty() => new(Guid.Empty);
    public static Result<VolunteerId> Create(Guid value)
    {
        if (value == Guid.Empty)
            return "Id cannot be empty";
        var id = new VolunteerId(value);
        return new VolunteerId(value);
    }
}

