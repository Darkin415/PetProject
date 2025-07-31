using FluentValidation;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Contracts;
using PetProject.Contracts.Validation;
using PetProject.Contracts.ValueObjects;

namespace PetProejct.Volunteers.Application.Volunteers.UpdateMainInfo;

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
