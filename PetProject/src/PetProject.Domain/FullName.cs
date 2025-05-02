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
    }
}

