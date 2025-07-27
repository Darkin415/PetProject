// using FluentAssertions;
// using PetProject.Application.Volunteers;
// using PetProject.Domain;
// using PetProject.Domain.Enum;
// using PetProject.Domain.PetSpecies;
// using PetProject.Domain.Shared.Ids;
// using PetProject.Domain.Shared.ValueObjects;
// using PetProject.Domain.Volunteers;
// using System;
// using System.Security.Cryptography.X509Certificates;
//
// namespace PetProject.Domain.UnitTests;
//
// public class VolunteerTests
// {
//
//
//
//     [Fact]
//     public void Add_Pet_With_Empty_Pets_Return_Success_Result()
//     {
//
//         // arrange - подготовка к тесту
//
//
//         var fullName = FullName.Create("Test", "Test", "Test").Value;
//
//         var email = Email.Create("Test").Value;
//
//         var description = Description.Create("Test").Value;
//
//         var telephonNumber = TelephonNumber.Create("89282809307").Value;
//
//         var socialMedias = SocialMedia.Create("Test", "Test");
//
//         var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber, null);
//
//         var nickName = NickName.Create("test").Value;
//
//         var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;
//
//         var color = Color.Create("test").Value;
//
//         var statusHealth = StatusHealth.Create("test").Value;
//
//         var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;
//
//         var castrationStatus = CastrationStatus.Create("test").Value;
//
//         var startDate = new DateTime(2010, 1, 1);
//
//         var endDate = new DateTime(2023, 12, 31);
//
//         var range = (endDate - startDate).Days;
//
//         var randomDate = startDate.AddDays(new Random().Next(range));
//
//         var birthday = BirthDay.Create(randomDate).Value;
//
//         var vaccinationStatus = VaccinationStatus.Create("Test").Value;
//
//         var statusHelp = StatusHelp.SeekingHome;
//
//         var specificDate = new DateTime(2023, 6, 15);
//
//         var dateOfCreation = DateOfCreation.Create(specificDate).Value;
//
//         var petId = PetId.NewPetId();
//         var pet = new Pet(petId,
//             nickName,
//             petInfo,
//             color,
//             statusHealth,
//             physicalAttribute,
//             telephonNumber,
//             castrationStatus,
//             birthday,
//             vaccinationStatus,
//             statusHelp,
//             dateOfCreation);
//         // act - вызов тестируемого метода
//
//         var result = volunteer.AddPet(pet);
//
//         // assert - проверка результата 
//
//         var addPetResult = volunteer.GetPetById(petId);
//
//         result.IsSuccess.Should().BeTrue();
//         addPetResult.IsSuccess.Should().BeTrue();
//         addPetResult.Value.Id.Should().Be(pet.Id);
//         addPetResult.Value.Position.Should().Be(Position.First);
//
//     }
//
//     [Fact]
//     public void Add_Pet_With_Other_Pets_Return_Success_Result()
//     {
//
//         // arrange - подготовка к тесту
//         const int petsCount = 5;
//         var volunteer = CreateVolunteerWithPet(0);
//         var fullName = FullName.Create("Test", "Test", "Test").Value;
//         var email = Email.Create("Test").Value;
//         var description = Description.Create("Test").Value;
//         var telephonNumber = TelephonNumber.Create("89282809307").Value;
//         var socialMedias = SocialMedia.Create("Test", "Test");
//
//
//
//         var nickName = NickName.Create("test").Value;
//         var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;
//         var color = Color.Create("test").Value;
//         var statusHealth = StatusHealth.Create("test").Value;
//         var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;
//         var castrationStatus = CastrationStatus.Create("test").Value;
//
//         var startDate = new DateTime(2010, 1, 1);
//         var endDate = new DateTime(2023, 12, 31);
//         var range = (endDate - startDate).Days;
//         var randomDate = startDate.AddDays(new Random().Next(range));
//         var birthday = BirthDay.Create(randomDate).Value;
//         var vaccinationStatus = VaccinationStatus.Create("Test").Value;
//         var statusHelp = StatusHelp.SeekingHome;
//         var specificDate = new DateTime(2023, 6, 15);
//         var dateOfCreation = DateOfCreation.Create(specificDate).Value;
//
//         var pets = Enumerable.Range(1, petsCount).Select(_ =>
//        new Pet(PetId.NewPetId(),
//            nickName,
//            petInfo,
//            color,
//            statusHealth,
//            physicalAttribute,
//            telephonNumber,
//            castrationStatus,
//            birthday,
//            vaccinationStatus,
//            statusHelp,
//            dateOfCreation));
//
//         var petToAdd = new Pet(PetId.NewPetId(),
//             nickName,
//             petInfo,
//             color,
//             statusHealth,
//             physicalAttribute,
//             telephonNumber,
//             castrationStatus,
//             birthday,
//             vaccinationStatus,
//             statusHelp,
//             dateOfCreation);
//
//         foreach (var pet in pets)
//             volunteer.AddPet(pet);
//
//         // act - вызов тестируемого метода
//
//         var result = volunteer.AddPet(petToAdd);
//
//         // assert - проверка результата 
//         var addPetResult = volunteer.GetPetById(petToAdd.Id);
//
//         var position = Position.Create(petsCount + 1).Value;
//
//         result.IsSuccess.Should().BeTrue();
//         addPetResult.IsSuccess.Should().BeTrue();
//         addPetResult.Value.Id.Should().Be(petToAdd.Id);
//         addPetResult.Value.Position.Value.Should().Be(position.Value);
//     }
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Not_Move_When_Pet_Already_Add_New_Position()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var secondPosition = Position.Create(2).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act
//
//         var result = volunteer.MovePet(secondPet, secondPosition);
//
//         //assert
//
//         result.IsSuccess.Should().BeTrue();
//         firstPet.Position.Value.Should().Be(1);
//         secondPet.Position.Value.Should().Be(2);
//         thirdPet.Position.Value.Should().Be(3);
//         fourthPet.Position.Value.Should().Be(4);
//         fifthPet.Position.Value.Should().Be(5);
//     }
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Move_Other_Pets_Back_When_Pet_New_Position_Is_Lower()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var secondPosition = Position.Create(2).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act
//
//         var result = volunteer.MovePet(fourthPet, secondPosition);
//
//         //assert 1 2 3 4 5 
//
//         result.IsSuccess.Should().BeTrue();
//         firstPet.Position.Value.Should().Be(1);
//         secondPet.Position.Value.Should().Be(3);
//         thirdPet.Position.Value.Should().Be(4);
//         fourthPet.Position.Value.Should().Be(2);
//         fifthPet.Position.Value.Should().Be(5);
//     }
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Move_Other_Pets_Back_When_Pet_New_Position_Is_Greater()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var secondPosition = Position.Create(4).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act
//
//         var result = volunteer.MovePet(secondPet, secondPosition);
//
//         //assert 1 2 3 4 5   1 3 4 2 5
//
//         result.IsSuccess.Should().BeTrue();
//         firstPet.Position.Value.Should().Be(1);
//         secondPet.Position.Value.Should().Be(4);
//         thirdPet.Position.Value.Should().Be(2);
//         fourthPet.Position.Value.Should().Be(3);
//         fifthPet.Position.Value.Should().Be(5);
//     }
//
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Move_Other_Pets_Forward_When_Pet_New_Position_Is_First()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var firstPosition = Position.Create(1).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act 
//
//         var result = volunteer.MovePet(fifthPet, firstPosition);
//
//         //assert 1 2 3 4 5   5 1 2 3 4 
//
//         result.IsSuccess.Should().BeTrue();
//         firstPet.Position.Value.Should().Be(2);
//         secondPet.Position.Value.Should().Be(3);
//         thirdPet.Position.Value.Should().Be(4);
//         fourthPet.Position.Value.Should().Be(5);
//         fifthPet.Position.Value.Should().Be(1);
//     }
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Move_Other_Pets_Back_When_Pet_New_Position_Is_Last()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var fifthPosition = Position.Create(5).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act 
//
//         var result = volunteer.MovePet(firstPet, fifthPosition);
//
//         //assert 1 2 3 4 5   2 3 4 5 1
//
//         result.IsSuccess.Should().BeTrue();
//         firstPet.Position.Value.Should().Be(5);
//         secondPet.Position.Value.Should().Be(1);
//         thirdPet.Position.Value.Should().Be(2);
//         fourthPet.Position.Value.Should().Be(3);
//         fifthPet.Position.Value.Should().Be(4);
//     }
//
//     [Fact]
//
//     public void Move_Pet_Shoud_Move_Other_Pets_Back_When_Some_Kind_Pet_Is_Deleted()
//     {
//         // arrange 
//
//         const int petsCount = 5;
//
//         var volunteer = CreateVolunteerWithPet(petsCount);
//
//         var fifthPosition = Position.Create(5).Value;
//
//         var firstPet = volunteer.Pets[0];
//         var secondPet = volunteer.Pets[1];
//         var thirdPet = volunteer.Pets[2];
//         var fourthPet = volunteer.Pets[3];
//         var fifthPet = volunteer.Pets[4];
//
//         //act 
//
//         var result = volunteer.RemovePet(thirdPet.Id);
//
//         //assert 1 2 3 4 5   1 2 3 4 
//
//         result.IsSuccess.Should().BeTrue();
//         volunteer.Pets.Count.Should().Be(4);
//         volunteer.Pets.Select(p => p.Position.Value).Should().Equal(1, 2, 3, 4);
//         fourthPet.Position.Value.Should().Be(3);
//         fifthPet.Position.Value.Should().Be(4);
//     }
//
//     private Volunteer CreateVolunteerWithPet(int petCount)
//     {
//         var fullName = FullName.Create("Test", "Test", "Test").Value;
//         var email = Email.Create("Test").Value;
//         var description = Description.Create("Test").Value;
//         var telephonNumber = TelephonNumber.Create("89282809307").Value;
//         var socialMedias = SocialMedia.Create("Test", "Test");
//         var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber, null);
//
//
//         var nickName = NickName.Create("test").Value;
//         var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;
//         var color = Color.Create("test").Value;
//         var statusHealth = StatusHealth.Create("test").Value;
//         var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;
//         var castrationStatus = CastrationStatus.Create("test").Value;
//
//         var startDate = new DateTime(2010, 1, 1);
//         var endDate = new DateTime(2023, 12, 31);
//         var range = (endDate - startDate).Days;
//         var randomDate = startDate.AddDays(new Random().Next(range));
//         var birthday = BirthDay.Create(randomDate).Value;
//         var vaccinationStatus = VaccinationStatus.Create("Test").Value;
//         var statusHelp = StatusHelp.SeekingHome;
//         var specificDate = new DateTime(2023, 6, 15);
//         var dateOfCreation = DateOfCreation.Create(specificDate).Value;
//
//         for (var i = 0; i < petCount; i++)
//         {
//             var pet = new Pet(PetId.NewPetId(),
//            nickName,
//            petInfo,
//            color,
//            statusHealth,
//            physicalAttribute,
//            telephonNumber,
//            castrationStatus,
//            birthday,
//            vaccinationStatus,
//            statusHelp,
//            dateOfCreation);
//
//             volunteer.AddPet(pet);
//         }
//
//         return volunteer;
//     }
// }
