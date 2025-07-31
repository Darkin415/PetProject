using FluentValidation;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Contracts;
using PetProject.Contracts.Validation;

namespace PetProejct.Volunteers.Application.Pet.AddPetPhoto;

public class UploadPetPhotosCommandValidator : AbstractValidator<UploadPetPhotoCommand>
{
    public UploadPetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
