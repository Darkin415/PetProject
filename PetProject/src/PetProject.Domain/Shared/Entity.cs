namespace PetProject.Domain.Shared;

public abstract class Entity<TId> where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }
    public TId Id { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;
        var other = (Shared.Entity<TId>)obj;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().FullName + Id).GetHashCode();
    }
}
