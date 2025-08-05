using FluentValidation;
using PetProject.Core.Validation;
using PetProject.Core.ValueObject;
using PetProject.SharedKernel;
using PetProject.Volunteers.Domain.VolunteerValueObject;

namespace PetProejct.Volunteers.Application.Commands.UpdateVolunteerMainInfo;

public class UpdateMainInfoHandlerCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoHandlerCommandValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(r => r.TelephonNumber).MustBeValueObject(TelephonNumber.Create);
        RuleFor(r => r.Description).MustBeValueObject(Description.Create);
    }
}
