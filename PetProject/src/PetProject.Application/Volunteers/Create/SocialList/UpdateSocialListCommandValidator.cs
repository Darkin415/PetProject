using FluentValidation;
using PetProject.Application.Commands;
using PetProject.Application.Validation;
using PetProject.Contracts.Commands;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.SocialList;

public class UpdateSocialListCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialListCommandValidator()
    {
        RuleForEach(c => c.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
    }
}
