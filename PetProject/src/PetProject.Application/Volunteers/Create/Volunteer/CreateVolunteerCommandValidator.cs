using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Domain;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.Volunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<AddVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.Request).NotNull();
        RuleFor(c => c.Request.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Request.Information).MustBeValueObject(Description.Create);
        RuleFor(c => c.Request.PhoneNumber).MustBeValueObject(TelephonNumber.Create);
        RuleForEach(c => c.Request.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
        RuleFor(c => c.Request.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
    }
}
