using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.API.Module;
using PetProject.Domain;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
namespace PetProject.Application.Volunteers.CreateVolunteer;

public record AddVolunteerCommand(
   
    CreateVolunteerRequest Request
);

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<AddVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<AddVolunteerCommand> validator, 
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Handle(AddVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        var volunteerId = VolunteerId.NewVolunteerId();
        var emailResult = Email.Create(command.Request.LinkMedia);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var descriptionResult = Description.Create(command.Request.Information);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var phoneResult = TelephonNumber.Create(command.Request.PhoneNumber);
        if (phoneResult.IsFailure)
            return phoneResult.Error;

        var fullNameResult = FullName.Create(command.Request.FullName.FirstName, command.Request.FullName.LastName, command.Request.FullName.Surname);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var socialMediaResults = command.Request.SocialMedias
        .Select(dto => SocialMedia.Create(dto.Title, dto.LinkMedia))
        .ToList();
        if (socialMediaResults.Any(r => r.IsFailure))

            return Result.Failure<Guid, Error>(socialMediaResults.First(r => r.IsFailure).Error);

        var socialMedias = socialMediaResults.Select(r => r.Value).ToList();

        var volunteer = await _volunteersRepository.GetByEmail(emailResult.Value);

        if (volunteer.IsSuccess)
            return Errors.Volunteer.AlreadyExist();

        var volunteerToCreate = new Volunteer(volunteerId, fullNameResult.Value, emailResult.Value, descriptionResult.Value, phoneResult.Value, socialMedias);
        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);
        _logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId);

        return (Guid)volunteerToCreate.Id;
    }
}