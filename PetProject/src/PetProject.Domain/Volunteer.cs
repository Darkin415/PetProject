using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Pet;
namespace PetProject.Domain
{
   public  class Volunteer : Shared.Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {

        }
        private readonly List<SocialMedia> _socialMedias = new List<SocialMedia>();
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;

        public FullName FullName { get; private set; }
        public SocialMediaList SocialList { get; private set; } // создал свойство для того чтобы потом через него сделать конфигурацию Jsonb
      
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public List<Pet> Pets = [];
        public TelephonNumber TelephoneNumber { get; private set; }

        //public Volunteer(VolunteerId volunteerId, string Email, string Description,  ) : base(volunteerId) сделать после всех value object
        //{
            
        //}
        public int CountPetFoundHome(List<Pet> pets)
        {
            return Pets?.Count(pet => pet.Status == StatusHelp.FoundedHome) ?? 0;
        }
        public int LookingHouse(List<Pet> pets)
        {
            return Pets?.Count(pet => pet.Status == StatusHelp.SeekingHome) ?? 0;
        }
        public int CountBeUnderTreatment(List<Pet> pets)
        {
            return Pets?.Count(pet => pet.Status == StatusHelp.BeUnderTreatment) ?? 0;
        }
    }
}
