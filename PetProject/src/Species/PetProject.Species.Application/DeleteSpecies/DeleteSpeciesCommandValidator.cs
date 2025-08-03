using FluentValidation;

using PetProject.Core.Validation;
using PetProject.SharedKernel;

namespace PetProject.Species.Application.DeleteSpecies;

public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesCommandValidator()
    {
        RuleFor(u => u.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
    }
}