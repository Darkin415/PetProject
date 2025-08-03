using FluentValidation;

using PetProject.Core.Validation;
using PetProject.SharedKernel;

namespace PetProject.Species.Application.DeleteBreed;

public class DeleteBreedCommandValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedCommandValidator()
    {
        RuleFor(u => u.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}