using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Contracts.Command;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

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
