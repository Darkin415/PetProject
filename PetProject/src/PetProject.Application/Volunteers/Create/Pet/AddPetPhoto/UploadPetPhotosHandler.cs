using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.FileProvider;
using PetProject.Application.Providers;
using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;

public class UploadPetPhotosHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IFilesProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public UploadPetPhotosHandler(
        IFilesProvider fileProvider,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;       
    }            
    public async Task<Result<List<string>, Error>> Handle(
        UploadPetPhotoCommand command, CancellationToken cancellationToken = default)
    {      
        var volunteerId = VolunteerId.Create(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId.Value, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            _logger.LogError("Волонтер не найден. VolunteerId: {VolunteerId}", command.VolunteerId);
            return volunteerResult.Error;
        }
            

        var petId = PetId.Create(command.PetId);

        var petResult = _volunteersRepository.GetByPetId(petId.Value);
        if (petResult.IsFailure)
        {
            _logger.LogError("Питомец не найден. PetId: {PetId}", command.PetId);
            return petResult.Error;
        }           

        List<FileData> photosData = [];

        foreach (var photo in command.Photos)
        {
            var extensions = Path.GetExtension(photo.FileName);

            var photoPath = FilePath.Create(Guid.NewGuid(), extensions);
            if (photoPath.IsFailure)
            {
                _logger.LogError("Ошибка создания пути. Файл: {FileName}", photo.FileName);
                return photoPath.Error;
            }                               

            var photoContent = new FileData(photo.Content, photoPath.Value, BUCKET_NAME);

            photosData.Add(photoContent);          
        }

        var uploadResult = await _fileProvider.UploadFiles(photosData, cancellationToken);
        if (uploadResult.IsFailure)
        {
            _logger.LogError("Ошибка загрузки в MinIO: {Error}", uploadResult.Error);
            return uploadResult.Error;
        }
            

        var petPhotos = photosData
            .Select(f => f.FilePath)
            .Select(f => new Photos(f))
            .ToList();

        var addPhotosResult = petResult.Value.AddPhotos(petPhotos);
        if(addPhotosResult.IsFailure)
        {
            _logger.LogError("Ошибка добавления фото в БД: {Error}", addPhotosResult.Error);
            return addPhotosResult.Error;
        }

        _logger.LogInformation("Files upload completed");

        var filesPaths = photosData.Select(x => x.FilePath.Path);

        return filesPaths.ToList();
    }
}


    


