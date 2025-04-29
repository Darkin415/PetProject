namespace PetProject.Domain
{
    public record TelephonNumber
    {
        private TelephonNumber(int value)
        {
            Value = value;
        }
        public int Value { get; }
        public string NumberPhone { get; }

        
    }
}