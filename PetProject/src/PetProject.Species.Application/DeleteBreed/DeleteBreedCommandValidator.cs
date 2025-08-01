using FluentValidation;
using PetProject.Contracts;
using PetProject.Contracts.Validation;
using PetProject.SharedKernel;

namespace PetProject.Species.Application.DeleteBreed;

public class DeleteBreedCommandValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedCommandValidator()
    {
        RuleFor(u => u.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}