namespace PetProject.Domain
{
    public record TelephonNumber
    {
        public TelephonNumber(string value)
        {
            Value = value;
            
        }
        public string Value { get; }
      

        
    }
}