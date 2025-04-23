using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Pet;
namespace PetProject.Domain
{
   public partial class Volunteer
    {
        public VolunteerId Id { get; private set; }
        public string Fullname { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public List<Pet> Pets = [];
        public string NumberPhone { get; private set; } = default!;
        public Details? ContactDetails { get; private set; }
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
