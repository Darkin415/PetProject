
using FluentValidation;
using PetProject.Core.Validation;
using PetProject.Volunteers.Domain.VolunteerValueObject;

namespace PetProejct.Volunteers.Application.Commands.UpdateSocialNetwork;

public class UpdateSocialListCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialListCommandValidator()
    {
        RuleForEach(c => c.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
    }
}
