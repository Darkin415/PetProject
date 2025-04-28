using CSharpFunctionalExtensions;
namespace PetProject.Domain
{
    public record Details
    {
        private Details(string telephonNumber)
        {
            TelephoneNumber = telephonNumber;
        }
        public string TelephoneNumber { get; }

        public static Result<Details> Create(string telephonNumber)
        {
            if(String.IsNullOrWhiteSpace(telephonNumber))
            {
                return Result.Failure<Details>("Number cannot be null");
            }
            else
            {
                return new Details(telephonNumber);
            }
        }
    }
}