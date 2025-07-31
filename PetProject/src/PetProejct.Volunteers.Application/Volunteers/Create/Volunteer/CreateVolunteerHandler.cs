using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Application.Extensions;
using PetProject.Contracts;
using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Ids;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain.Pets;

namespace PetProejct.Volunteers.Application.Volunteers.Create.Volunteer;

public class CreateVolunteerHandler : ICommandHandler<Guid, AddVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IValidator<AddVolunteerCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<AddVolunteerCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;      
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(AddVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
            
        var volunteerId = VolunteerId.NewVolunteerId();

        var emailResult = Email.Create(command.LinkMedia);
        if (emailResult.IsFailure)
            return emailResult.Error.ToErrorList();

        var descriptionResult = Description.Create(command.Information);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error.ToErrorList();

        var phoneResult = TelephonNumber.Create(command.PhoneNumber);
        if (phoneResult.IsFailure)
            return phoneResult.Error.ToErrorList();

        var fullNameResult = FullName.Create(
            command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Surname);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error.ToErrorList();

        var socialMediaResults = command.SocialMedias
        .Select(dto => SocialMedia.Create(dto.Title, dto.LinkMedia))
        .ToList();
        if (socialMediaResults.Any(r => r.IsFailure))

            return Result.Failure<Guid, ErrorList>(socialMediaResults.First(r => r.IsFailure).Error);

        var socialMediasList = socialMediaResults
    .Select(r => r.Value)
    .ToList();

        var volunteer = await _volunteersRepository.GetByEmail(emailResult.Value);

        if (volunteer.IsSuccess)
        {
            var error = Errors.General.AlreadyExist();
            var errorList = new ErrorList(new[] { error }); 
            return Result.Failure<Guid, ErrorList>(errorList);
        }
            

        var volunteerToCreate = new PetProject.Volunteers.Domain.Volunteer(
            volunteerId,
            fullNameResult.Value,
            emailResult.Value,
            descriptionResult.Value,
            phoneResult.Value,
            socialMediasList);

        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId);

        return (Guid)volunteerToCreate.Id;
    }
}