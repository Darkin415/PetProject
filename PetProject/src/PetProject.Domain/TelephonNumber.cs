using CSharpFunctionalExtensions;

namespace PetProject.Domain
{
    public record TelephonNumber
    {
        public string Value { get; }
        public TelephonNumber(string value)
        {
            Value = value;
        }
       public static Result<TelephonNumber> Create(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<TelephonNumber>("Number can not be empty");
                
            }
            else
            {
                var number =  new TelephonNumber(value);
                return Result.Success<TelephonNumber>(number);
            }
        }
    }
}