using CSharpFunctionalExtensions;

namespace PetProject.Domain
{
    public record FullName
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Surname { get; }

        private FullName(string firstName, string lastName, string surname)
        {
            FirstName = firstName;
            LastName = lastName;
            Surname = surname;
        }
        public static Result<FullName> Create(string firstName, string lastName, string surname)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<FullName>("Firstname can not be empty");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<FullName>("Lastname can not be empty");
            if (string.IsNullOrWhiteSpace(surname))
                return Result.Failure<FullName>("Surname can not be empty");
            else
            {
                var fullname = new FullName(firstName, lastName, surname); 
                return Result.Success<FullName>(fullname);
            }
        }
    }
}

