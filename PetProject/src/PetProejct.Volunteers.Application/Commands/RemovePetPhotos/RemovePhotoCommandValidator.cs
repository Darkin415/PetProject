using FluentValidation;
using PetProject.Contracts;
using PetProject.Contracts.Validation;
using PetProject.SharedKernel;
using PetProject.SharedKernel.ValueObjects;

namespace PetProejct.Volunteers.Application.Commands.RemovePetPhotos;

public class RemovePetPhotosCommandValidator : AbstractValidator<RemovePetPhotosCommand>
{
    public RemovePetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.PhotoNames).MustBeValueObject(FilePath.Create);
    }
}
