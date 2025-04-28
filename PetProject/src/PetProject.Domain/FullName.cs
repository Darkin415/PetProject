namespace PetProject.Domain
{
    public record FullName
    {
        private FullName(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static FullName NewVolunteerId() => new(Guid.NewGuid());
        public static FullName Empty() => new(Guid.Empty);
    }
}

