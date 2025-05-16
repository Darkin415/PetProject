using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Domain;
public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = new List<Pet>();
    private Volunteer(VolunteerId id) : base(id)
    {

    }
    public Volunteer(
    VolunteerId id,
    FullName fullName,
    Email email,
    Description description,
    TelephonNumber telephoneNumber,
    IReadOnlyList<SocialMedia>? socialMedias = null

) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        TelephonNumber = telephoneNumber;
    }
    
    public FullName FullName { get; private set; }
    public SocialMediaList SocialList { get; private set; }
    public Email Email { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public IReadOnlyList<Pet> Pets => _pets;
    public TelephonNumber TelephonNumber { get; private set; }
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
    public static Result<Volunteer, string> Create(Email email, Description description, FullName fullName, TelephonNumber telephonNumber, IReadOnlyList<SocialMedia>? socialMedias)
    {
        if (email == null)
        {
            return "Email can not be empty";
        }
        if (description == null)
        {
            return "Description can not be empty";
        }
        else
        {
            var volunteer = new Volunteer(
            VolunteerId.NewVolunteerId(),
            fullName,
            email,
            description,
            telephonNumber,
            socialMedias
        );
            return volunteer;
        }
    }
}
