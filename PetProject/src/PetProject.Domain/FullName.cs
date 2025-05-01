namespace PetProject.Domain
{
    public record FullName
    {
        
        private FullName(int value)
        {
            Value = value;
        }
        public int Value { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Surname { get; }
        
    }
}

