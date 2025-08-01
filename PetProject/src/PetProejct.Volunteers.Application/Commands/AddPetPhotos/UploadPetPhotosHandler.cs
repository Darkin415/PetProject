using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Contracts.Ids;
using PetProject.Core.Abstraction;
using PetProject.Core.Database;
using PetProject.Core.Messaging;
using PetProject.Files.Contracts;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;
using PetProject.Volunteers.Domain.PetValueObjects;
using FileInfo = PetProject.SharedKernel.ValueObjects.FileInfo;

namespace PetProejct.Volunteers.Application.Commands.AddPetPhotos;

public class UploadPetPhotosHandler : ICommandHandler<Guid, UploadPetPhotoCommand>
{
    private const string BUCKET_NAME = "photos";    
    private readonly ILogger<UploadPetPhotosHandler> _logger;
    private readonly IFilesContract _filesContract;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UploadPetPhotoCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IUnitOfWork _unitOfWork;  

    public UploadPetPhotosHandler(
        IFilesContract filesContract,
        IUnitOfWork unitOfWOrk,
        IValidator<UploadPetPhotoCommand> validator,
        IVolunteersRepository volunteersRepository,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ILogger<UploadPetPhotosHandler> logger)
    {        
        _logger = logger;
        _unitOfWork = unitOfWOrk;
        _validator = validator;
        _filesContract = filesContract;
        _volunteersRepository = volunteersRepository;
        _messageQueue = messageQueue;
    }            
    public async Task<Result<Guid, ErrorList>> Handle(
        UploadPetPhotoCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        var volunteerId = VolunteerId.Create(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId.Value, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            _logger.LogError("Волонтер не найден. VolunteerId: {VolunteerId}", command.VolunteerId);
            return volunteerResult.Error.ToErrorList();
        }
            

        var petId = PetId.Create(command.PetId);

        var petResult = _volunteersRepository.GetByPetId(petId.Value);
        if (petResult.IsFailure)
        {
            _logger.LogError("Питомец не найден. PetId: {PetId}", command.PetId);
            return petResult.Error.ToErrorList();
        }           

        List<FileData> photosData = [];

        foreach (var photo in command.Photos)
        {
            var extensions = Path.GetExtension(photo.FileName);

            var photoPath = FilePath.Create(Guid.NewGuid(), extensions);
            if (photoPath.IsFailure)
            {
                _logger.LogError("Ошибка создания пути. Файл: {FileName}", photo.FileName);
                return photoPath.Error.ToErrorList();
            }                               

            var photoContent = new FileData(photo.Content, new FileInfo(photoPath.Value, BUCKET_NAME));

            photosData.Add(photoContent);          
        }

        var uploadResult = await _filesContract.UploadFiles(photosData, cancellationToken);
        if (uploadResult.IsFailure)
        {
            await _messageQueue.WriteAsync(photosData.Select(p => p.Info), cancellationToken);
            
            return uploadResult.Error.ToErrorList();
        }
            
        var petPhotos = photosData
            .Select(f => f.Info.FilePath)
            .Select(f => new Photos(f))
            .ToList();

        var addPhotosResult = petResult.Value.AddPhotos(petPhotos);

        await _unitOfWork.SaveChanges(cancellationToken);
        if (addPhotosResult.IsFailure)
        {
            _logger.LogError("Ошибка добавления фото в БД: {Error}", addPhotosResult.Error);
            return addPhotosResult.Error.ToErrorList();
        }

        _logger.LogInformation("Files upload completed");

        var filesPaths = photosData.Select(x => x.Info.FilePath.Path);

        return petResult.Value.Id.Value;
    }
}