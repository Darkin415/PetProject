using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Application.Volunteers.Create.SocialList.UpdateSocialListHandler;

namespace PetProject.Application.Volunteers.Create.SocialList;

public class UpdateSocialListCommandValidator : AbstractValidator<UpdateSocialListCommand>
{
    public UpdateSocialListCommandValidator()
    {
        RuleForEach(c => c.Request.SocialMedias).MustBeValueObject(s => SocialMedia.Create(s.Title, s.LinkMedia));
    }
}
