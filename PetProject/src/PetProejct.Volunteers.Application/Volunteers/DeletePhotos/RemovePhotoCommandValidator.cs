using FluentValidation;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Application.Providers;
using PetProject.Contracts;
using PetProject.Contracts.Validation;

namespace PetProejct.Volunteers.Application.Volunteers.DeletePhotos;

public class RemovePetPhotosCommandValidator : AbstractValidator<RemovePetPhotosCommand>
{
    public RemovePetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(c => c.PhotoNames).MustBeValueObject(FilePath.Create);
    }
}
