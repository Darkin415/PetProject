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
    public class Volunteer : Shared.Entity<VolunteerId>
    {
        public FullName FullName { get; private set; }
        public SocialMediaList SocialList { get; private set; }

        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        private readonly List<Pet> _pets = new List<Pet>();
        public IReadOnlyList<Pet> Pets => _pets;
        public TelephonNumber TelephonNumber { get; private set; }
        private Volunteer(VolunteerId id) : base(id)
        {

        }
        public Volunteer(
        VolunteerId id,
        FullName fullName,
        string email,
        string description,
        TelephonNumber telephoneNumber,
        IReadOnlyList<SocialMedia>? socialMedias = null,
        IReadOnlyList<Pet>? pets = null
    ) : base(id)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            TelephonNumber = telephoneNumber;
            _pets = pets?.ToList() ?? new List<Pet>();
        }

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
