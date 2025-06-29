using PetProject.Application.Volunteers;
using PetProject.Domain;
using PetProject.Domain.Enum;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PetProject.UnitTests;

public class VolunteerTests
{
    private readonly IVolunteersRepository _volunteerRepository;

    public VolunteerTests(IVolunteersRepository volunteersRepository)
    {
        _volunteerRepository = volunteersRepository;
    }
    [Fact]
    public void Add_Pet_With_Empty_Pets_Approach_Return_Success_Result()
    {

        // arrange - подготовка к тесту
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var email = Email.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var telephonNumber = TelephonNumber.Create("Test").Value;
        var socialMedias = SocialMedia.Create("Test", "Test");
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber, null);
        var petId = PetId.NewPetId();


        var nickName = NickName.Create("test").Value;
        var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;
        var color = Color.Create("test").Value;
        var statusHealth = StatusHealth.Create("test").Value;
        var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;
        var castrationStatus = CastrationStatus.Create("test").Value;

        var startDate = new DateTime(2010, 1, 1);
        var endDate = new DateTime(2023, 12, 31);
        var range = (endDate - startDate).Days;
        var randomDate = startDate.AddDays(new Random().Next(range));
        var birthday = BirthDay.Create(randomDate).Value;
        var vaccinationStatus = VaccinationStatus.Create("Test").Value;
        var statusHelp = StatusHelp.SeekingHome;
        var specificDate = new DateTime(2023, 6, 15);
        var dateOfCreation = DateOfCreation.Create(specificDate).Value;

        var pet = new Pet(
            petId,
            nickName,
            petInfo,
            color,
            statusHealth,
            physicalAttribute,
            telephonNumber,
            castrationStatus,
            birthday,
            vaccinationStatus,
            statusHelp,
            dateOfCreation);
        // act - вызов тестируемого метода

        var result = volunteer.AddPet(pet);

        // assert - проверка результата 

        var addPetResult = _volunteerRepository.GetByPetId(petId);

        Assert.True(result.IsSuccess);
        Assert.True(addPetResult.IsSuccess);
        Assert.Equal(addPetResult.Value.Id, pet.Id);
        Assert.Equal(addPetResult.Value.SerialNumber, SerialNumber.First);


    }

    [Fact]
    public void Add_Pet_With_Other_Pets_Approach_Return_Success_Result()
    {

        // arrange - подготовка к тесту
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var email = Email.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var telephonNumber = TelephonNumber.Create("Test").Value;
        var socialMedias = SocialMedia.Create("Test", "Test");
        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber, null);


        var nickName = NickName.Create("test").Value;
        var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;
        var color = Color.Create("test").Value;
        var statusHealth = StatusHealth.Create("test").Value;
        var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;
        var castrationStatus = CastrationStatus.Create("test").Value;

        var startDate = new DateTime(2010, 1, 1);
        var endDate = new DateTime(2023, 12, 31);
        var range = (endDate - startDate).Days;
        var randomDate = startDate.AddDays(new Random().Next(range));
        var birthday = BirthDay.Create(randomDate).Value;
        var vaccinationStatus = VaccinationStatus.Create("Test").Value;
        var statusHelp = StatusHelp.SeekingHome;
        var specificDate = new DateTime(2023, 6, 15);
        var dateOfCreation = DateOfCreation.Create(specificDate).Value;

        var pet = new Pet(
            PetId.NewPetId(),
            nickName,
            petInfo,
            color,
            statusHealth,
            physicalAttribute,
            telephonNumber,
            castrationStatus,
            birthday,
            vaccinationStatus,
            statusHelp,
            dateOfCreation);
        // act - вызов тестируемого метода

        var result = volunteer.AddPet(pet);

        // assert - проверка результата 
    }
}
