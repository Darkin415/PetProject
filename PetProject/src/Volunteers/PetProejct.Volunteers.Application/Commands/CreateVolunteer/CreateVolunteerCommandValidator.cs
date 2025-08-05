using FluentValidation;
using PetProject.Core.Validation;
using PetProject.Core.ValueObject;
using PetProject.Volunteers.Domain.VolunteerValueObject;

namespace PetProejct.Volunteers.Application.Commands.CreateVolunteer;

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
