using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Volunteers.Volunteer;
namespace PetProject.Domain.Volunteers;
public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted = false;
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
    SocialMediaList? socialMedias = null

) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        TelephonNumber = telephoneNumber;
        SocialList = socialMedias ?? new SocialMediaList(new List<SocialMedia>());
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
    public static Result<Volunteer, string> Create(
        Email email,
        Description description,
        FullName fullName,
        TelephonNumber telephonNumber,
        SocialMediaList? socialMedias = null)
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

    public void UpdateMainInfo(FullName fullName, Description description, TelephonNumber telephonNumber)
    {
        FullName = fullName;
        Description = description;
        TelephonNumber = telephonNumber;
    }

    public void UpdateSocialList(SocialMediaList socialMedias)
    {
        SocialList = socialMedias;
    }

    public void Delete()
    {
        _isDeleted = true;

    }

    public void Restore()
    {
        _isDeleted = false;
    }
}
