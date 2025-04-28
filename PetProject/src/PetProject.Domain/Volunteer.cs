using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Pet;
namespace PetProject.Domain
{
   public partial class Volunteer
    {
        //для ef core
        private Volunteer()
        {
            
        }
        private readonly List<Pet> _pets = new List<Pet>();
        private readonly List<SocialMedia> _socialMedias = new List<SocialMedia>();

        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias.AsReadOnly();
        private Volunteer(
            VolunteerId id,
            TelephonNumber telephonNumber,
            string email, 
            string description)
        {
            Id = id;
            Email = email;
            Description = description;
            PhoneNumber = telephonNumber;
        }
        public SocialMedia socialMedia { get; private set; }
        public FullName fullName { get; private set; }
        public VolunteerId Id { get; private set; }
        public TelephonNumber PhoneNumber { get; private set; }
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();
        
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
