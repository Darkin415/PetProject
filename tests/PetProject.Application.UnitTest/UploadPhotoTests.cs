using PetProject.Contracts.Command;
using PetProject.Domain.Enum;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain;
using PetProject.Domain.Volunteers;
using System.Threading.Tasks;
using PetProject.Contracts.Dtos;
using PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;
using PetProject.Application.Providers;
using CSharpFunctionalExtensions;
using PetProject.Application.FileProvider;
using Moq;
using PetProject.Application.Volunteers;
using FluentValidation;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Threading;
using FluentValidation.Results;
using FluentAssertions;

namespace PetProject.Application.UnitTest;

//public class UploadPhotoTests
//{
    //[Fact]
    //public async Task Handle_Should_Return_Error_List_When_Validation_Failes_Photos_To_Pet()
    //{
    //    // arrange 

    //    var fullName = FullName.Create("Test", "Test", "Test").Value;

    //    var email = Email.Create("Test").Value;

    //    var description = Description.Create("Test").Value;

    //    var telephonNumber = TelephonNumber.Create("89282809307").Value;

    //    var nickName = NickName.Create("test").Value;

    //    var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;

    //    var color = Color.Create("test").Value;

    //    var statusHealth = StatusHealth.Create("test").Value;

    //    var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;

    //    var castrationStatus = CastrationStatus.Create("test").Value;

    //    var startDate = new DateTime(2010, 1, 1);

    //    var endDate = new DateTime(2023, 12, 31);

    //    var range = (endDate - startDate).Days;

    //    var randomDate = startDate.AddDays(new Random().Next(range));

    //    var birthday = BirthDay.Create(randomDate).Value;

    //    var vaccinationStatus = VaccinationStatus.Create("Test").Value;

    //    var statusHelp = StatusHelp.SeekingHome;

    //    var specificDate = new DateTime(2023, 6, 15);

    //    var ct = new CancellationTokenSource().Token;

    //    var dateOfCreation = DateOfCreation.Create(specificDate).Value;

    //    var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber);

    //    var pet = new Pet(PetId.NewPetId(),
    //        nickName,
    //        petInfo,
    //        color,
    //        statusHealth,
    //        physicalAttribute,
    //        telephonNumber,
    //        castrationStatus,
    //        birthday,
    //        vaccinationStatus,
    //        statusHelp,
    //        dateOfCreation);

    //    var stream = new MemoryStream();

    //    var createFileDto = new CreateFileDto(stream, "test.jpg");

    //    List<CreateFileDto> files = [createFileDto, createFileDto];

    //    var command = new UploadPetPhotoCommand(files, volunteer.Id.Value, pet.Id.Value);

    //    var fileProviderMock = new Mock<IFilesProvider>();

    //    List<FilePath> filePaths =
    //        [
    //        FilePath.Create("test.jpg").Value,
    //        FilePath.Create("test.jpg").Value
    //        ];

    //    fileProviderMock
    //        .Setup(v => v.UploadFiles(It.IsAny<List<FileData>>(), ct))
    //        .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));

    //    var volunteerRepositoryMock = new Mock<IVolunteersRepository>();

    //    volunteerRepositoryMock
    //        .Setup(v => v.GetVolunteerById(volunteer.Id, ct))
    //        .ReturnsAsync(volunteer);

    //    var validatorMock = new Mock<IValidator<UploadPetPhotoCommand>>();

    //    validatorMock.Setup(v => v.ValidateAsync(command, ct))
    //        .ReturnsAsync(new FluentValidation.Results.ValidationResult
    //        {
    //            Errors = new List<ValidationFailure>
    //            {
    //                new ValidationFailure("title", "title")
    //            }
    //        });

    //    var loggerMock = new Mock<ILogger<UploadPetPhotosHandler>>();

    //    var handler = new UploadPetPhotosHandler(
    //        fileProviderMock.Object,
    //        validatorMock.Object,
    //        volunteerRepositoryMock.Object,
    //        )

    //    //act

    //    var result = await handler.Handle(command, CancellationToken cancellationToken);

    //    //assert
    //    result.IsFailure.Should().BeTrue();
    //}



//    [Fact]
//    public async Task Handle_Should_Upload_Photos_To_Pet()
//    {
//        // arrange      

//        var fullName = FullName.Create("Test", "Test", "Test").Value;

//        var email = Email.Create("Test").Value;

//        var description = Description.Create("Test").Value;

//        var telephonNumber = TelephonNumber.Create("89282809307").Value;
        
//        var nickName = NickName.Create("test").Value;

//        var petInfo = PetInfo.Create(SpeciesId.NewSpeciesId(), BreedId.NewBreedId()).Value;

//        var color = Color.Create("test").Value;

//        var statusHealth = StatusHealth.Create("test").Value;

//        var physicalAttribute = PhysicalAttributes.Create(12, 12).Value;

//        var castrationStatus = CastrationStatus.Create("test").Value;

//        var startDate = new DateTime(2010, 1, 1);

//        var endDate = new DateTime(2023, 12, 31);

//        var range = (endDate - startDate).Days;

//        var randomDate = startDate.AddDays(new Random().Next(range));

//        var birthday = BirthDay.Create(randomDate).Value;

//        var vaccinationStatus = VaccinationStatus.Create("Test").Value;

//        var statusHelp = StatusHelp.SeekingHome;

//        var specificDate = new DateTime(2023, 6, 15);

//        var ct = new CancellationTokenSource().Token;

//        var dateOfCreation = DateOfCreation.Create(specificDate).Value;

//        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName, email, description, telephonNumber);
      
//        var pet = new Pet(PetId.NewPetId(),
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

//        var stream = new MemoryStream();

//        var createFileDto = new CreateFileDto(stream, "test.jpg");

//        List<CreateFileDto> files = [createFileDto, createFileDto];

//        var command = new UploadPetPhotoCommand(files, volunteer.Id.Value, pet.Id.Value);

//        var fileProviderMock = new Mock<IFilesProvider>();

//        List<FilePath> filePaths =
//            [
//            FilePath.Create("test.jpg").Value,
//            FilePath.Create("test.jpg").Value
//            ];

//        fileProviderMock
//            .Setup(v => v.UploadFiles(It.IsAny<List<FileData>>(), ct))
//            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));

//        var volunteerRepositoryMock = new Mock<IVolunteersRepository>();

//        volunteerRepositoryMock
//        .Setup(v => v.GetVolunteerById(volunteer.Id, ct))
//        .ReturnsAsync(volunteer);

//        volunteerRepositoryMock
//        .Setup(v => v.GetByPetId(pet.Id))
//        .Returns(Result.Success<Pet, Error>(pet));

//        var loggerMock = new Mock<ILogger<UploadPetPhotosHandler>>();


//        var validatorMock = new Mock<IValidator<UploadPetPhotoCommand>>();

//        validatorMock.Setup(v => v.ValidateAsync(command, ct))
//            .ReturnsAsync(new FluentValidation.Results.ValidationResult());


//        var handler = new UploadPetPhotosHandler(
//            fileProviderMock.Object,
//            validatorMock.Object,
//            volunteerRepositoryMock.Object,
//            loggerMock.Object);

//        //act

//        var result = await handler.Handle(command, ct);

//        //assert

//        result.IsSuccess.Should().BeTrue();
//        result.Value.Should().Be(pet.Id.Value);
//    }
   
//}
