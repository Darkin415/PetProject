using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Providers;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.DeletePhotos;

public class RemovePhotoHandler
{
    private readonly IFilesProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemovePhotoHandler> _logger;

    public RemovePhotoHandler(IFilesProvider fileProvider,
    IVolunteersRepository volunteersRepository,
    IUnitOfWork unitOfWork,
    IValidator<RemovePetPhotosCommand> validator,
    ILogger<RemovePhotoHandler> logger)
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
