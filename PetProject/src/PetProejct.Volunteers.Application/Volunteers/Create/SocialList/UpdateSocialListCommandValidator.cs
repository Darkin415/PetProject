using FluentValidation;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Contracts.Validation;
using PetProject.Volunteers.Domain.Pets;

namespace PetProejct.Volunteers.Application.Volunteers.Create.SocialList;

public class UpdateSocialListCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialListCommandValidator()
    {
        RuleForEach(c => c.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
    }
}
