using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.SocialList;

public class UpdateSocialListCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialListCommandValidator()
    {
        RuleForEach(c => c.Request.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
    }
}
