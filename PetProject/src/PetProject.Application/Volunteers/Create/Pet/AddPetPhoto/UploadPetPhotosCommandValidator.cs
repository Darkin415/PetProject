using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Contracts.Commands;
using PetProject.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;

public class UploadPetPhotosCommandValidator : AbstractValidator<UploadPetPhotoCommand>
{
    public UploadPetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
