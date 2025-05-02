namespace PetProject.Domain
{
    public record TelephonNumber
    {
        public string Value { get; }
        public TelephonNumber(string value)
        {
            Value = value;
        }
       
    }
}