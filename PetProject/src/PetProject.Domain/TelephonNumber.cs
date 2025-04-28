namespace PetProject.Domain
{
 
        public record TelephonNumber
        {
            private TelephonNumber(string telephonNumber)
            {
                TelephoneNumber = telephonNumber;
            }
            public string TelephoneNumber { get; }
        }
    }
