using PetProject.Domain.Shared.Ids;

namespace PetProject.Domain.Species
{
    public  class Breed
    {
        public BreedId Id { get; set; }
        public string Title { get; set; }
        private Breed(BreedId BreedId, string title) 
        {
            Title = title;
        }
    }
}
