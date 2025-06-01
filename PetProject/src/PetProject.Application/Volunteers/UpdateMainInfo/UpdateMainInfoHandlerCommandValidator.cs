using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandlerCommandValidator : AbstractValidator<AddUpdateMainInfoCommand>
{
    public UpdateMainInfoHandlerCommandValidator()
    {
        RuleFor(r => r.Request.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
