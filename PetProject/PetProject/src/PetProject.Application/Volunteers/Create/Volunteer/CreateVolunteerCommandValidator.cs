using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Contracts.Command;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.Volunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<AddVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {       
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Information).MustBeValueObject(Description.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(TelephonNumber.Create);
        RuleForEach(c => c.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
        RuleFor(c => c.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
    }
}
