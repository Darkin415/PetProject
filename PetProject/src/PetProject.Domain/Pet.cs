using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Domain
{
    public partial class Pet
    {
        public Guid Id { get; private set; }
        public string Nickname { get; private set; } = default!;
        public string View { get; private set; } = default!;
        public string Breed { get; private set; } = default!;
        public string Color { get; private  set; } = default!;
        public string StatusHealth { get; private set; } = default!;
        public  int Weight { get; private set; }
        public int Height { get; private  set; }
        public int TelephoneNumber { get; private set; }
        public string CastrationStatus { get; private set; } = default!;
        public DateTime BirthDate { get; private set; }
        public string VaccinationStatus { get; private set; } = default!;
        public Details ContactDetails { get; private set; }
        public SpeciesId SpeciesId { get; }
        public BreedId BreedId { get; }
        public enum StatusHelp { 
            FoundedHome, 
            SeekingHome, 
            BeUnderTreatment 
        };
        public StatusHelp Status { get; private set; }
        public DateTime DateOfCreation { get; private set; }
        public Pet(
            Guid id, 
            string nicname,
            string view, 
            string breed, 
            string color, 
            string statusHealth, 
            int weight, 
            int height, 
            int telephoneNumber, 
            string castrationStatus, 
            DateTime birthdate, 
            string vaccinationStatus,
            string statusHelp,
            StatusHelp status,
            DateTime dateOfCreation 
            )
        {
            Id = id;
            Nickname = nicname;
            View = view;
            Breed = breed;
            Color = color;
            Status = status;
            StatusHealth = statusHealth;
            Weight = weight;
            Height = height;
            TelephoneNumber = telephoneNumber;
            CastrationStatus = castrationStatus;
            BirthDate = birthdate;
            VaccinationStatus = vaccinationStatus;
            DateOfCreation = dateOfCreation;

        }

       
            
        }

    }

