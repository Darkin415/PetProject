using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetProject.API.Module;
using PetProject.Domain;
using PetProject.Domain.Shared;
namespace PetProject.Application.Volunteers.CreateVolunteer;
public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var emailResult = Email.Create(request.Link);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var descriptionResult = Description.Create(request.Information);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var phoneResult = TelephonNumber.Create(request.PhoneNumber);
        if (phoneResult.IsFailure)
            return phoneResult.Error;

        var fullNameResult = FullName.Create(request.FirstName, request.LastName, request.Surname);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var socialMediaResults = request.SocialMedias
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
        return (Guid)volunteerToCreate.Id;
    }
}