using FluentValidation;
using PetProject.Contracts.Commands;

namespace PetProject.Application.Volunteers.Delete;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
