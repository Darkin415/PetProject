using FluentValidation;
using PetProject.Contracts;
using PetProject.Contracts.Validation;
using PetProject.SharedKernel;

namespace PetProejct.Volunteers.Application.Commands.AddPetPhotos;

public class UploadPetPhotosCommandValidator : AbstractValidator<UploadPetPhotoCommand>
{
    public UploadPetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
