using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Application.Providers;
using PetProject.Contracts;
using PetProject.Contracts.Ids;
using PetProject.Volunteers.Domain.Pets;

namespace PetProejct.Volunteers.Application.Volunteers.DeletePhotos;

public class RemovePhotoHandler
{
    private readonly IFilesProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<RemovePhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePhotoHandler(IFilesProvider fileProvider,
    IVolunteersRepository volunteersRepository,    
    IValidator<RemovePetPhotosCommand> validator,
    ILogger<RemovePhotoHandler> logger,
    IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;      
    }
    public async Task<Result<List<string>, Error>> Handle(
        RemovePetPhotosCommand command, CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure) 
            return volunteerResult.Error;

        var petId = PetId.Create(command.PetId);

        var petResult = _volunteersRepository.GetByPetId(petId.Value);

        if (petResult.IsFailure)
            return petResult.Error;

        var removeResult = await _fileProvider.RemoveFiles(command.PhotoNames, cancellationToken); // удалил файлы из Minio
        if (removeResult.IsFailure) 
            return removeResult.Error;

        List<FilePath> photoPaths = [];

        foreach (var photoName in command.PhotoNames)
        {
            var photoPath = FilePath.Create(photoName);

            if (photoPath.IsFailure)           
                return photoPath.Error;
            
            photoPaths.Add(photoPath.Value);
        }

        var petPhotos = photoPaths
            .Select(f => new Photos(f))
            .ToList();

        var removePhotosResult = petResult.Value.RemovePhotos(petPhotos);
        if (removePhotosResult.IsFailure)
        {
            _logger.LogError("Failed to remove files from MinIO: {Error}", removeResult.Error);
            return removePhotosResult.Error;
        }

        await _unitOfWork.SaveChanges(cancellationToken);
      
        var photosPaths = photoPaths.Select(x => x.Path);

        return photosPaths.ToList();       
    }
}
