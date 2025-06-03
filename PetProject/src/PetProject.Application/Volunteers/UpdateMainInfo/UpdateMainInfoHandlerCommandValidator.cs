using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandlerCommandValidator : AbstractValidator<AddUpdateMainInfoCommand>
{
    public UpdateMainInfoHandlerCommandValidator()
    {
        RuleFor(r => r.Request.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(c => c.Request.FullName)
            .MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.Surname));
        RuleFor(r => r.Request.TelephonNumber).MustBeValueObject(TelephonNumber.Create);
        RuleFor(r => r.Request.Description).MustBeValueObject(Description.Create);
    }
}
