using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Core.Database;
using PetProject.Files.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Volunteers.Contracts.ids;
using PetProject.Volunteers.Domain.PetValueObjects;

namespace PetProejct.Volunteers.Application.Commands.RemovePetPhotos;

public class RemovePhotoHandler
{
    private readonly IFilesContract _fileContract;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<RemovePhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePhotoHandler(IFilesContract fileContract,
    IVolunteersRepository volunteersRepository,    
    IValidator<RemovePetPhotosCommand> validator,
    ILogger<RemovePhotoHandler> logger,
    IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileContract = fileContract;
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

        var removeResult = await _fileContract.RemoveFiles(command.PhotoNames, cancellationToken); // удалил файлы из Minio
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
