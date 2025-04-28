using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
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
   public partial class Volunteer : Shared.Entity<VolunteerId>
    {
        //для ef core
        private Volunteer(VolunteerId id) : base(id)
        {
            
        }
        private readonly List<Pet> _pets = new List<Pet>();
        private readonly List<SocialMedia> _socialMedias = new List<SocialMedia>();

        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
        private Volunteer(
            VolunteerId volunteerId,
            TelephonNumber telephonNumber,
            string email, 
            string description) : base (volunteerId)
        {
            Email = email;
            Description = description;
            PhoneNumber = telephonNumber;
        } 
        public SocialMedia SocialMedia { get; private set; }
        public FullName FullName { get; private set; }
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
