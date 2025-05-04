using CSharpFunctionalExtensions;

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
       
        public static Result<PetId> Create(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<PetId>("Id cannot be empty");
            else
            {
                var id = new PetId(value);
                return Result.Success(new PetId(value));
            }      
        }
    }
}