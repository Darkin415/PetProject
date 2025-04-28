namespace PetProject.Domain
{
    public record FullName
    {
        public FullName(string firstname, string lastname, string surname)
        {
            FirstName = firstname;
            LastName = lastname;
            Surname = surname;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Surname { get; }
    }
}
