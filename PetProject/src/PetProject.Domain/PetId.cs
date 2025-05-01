namespace PetProject.Domain
{
    public record PetId
    {
        public PetId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static PetId NewGuidId() => new(Guid.NewGuid());
        public static PetId Empty() => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);
    }
    }