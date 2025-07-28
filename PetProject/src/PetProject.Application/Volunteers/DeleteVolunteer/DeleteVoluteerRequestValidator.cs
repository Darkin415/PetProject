using FluentValidation;
using PetProject.Application.Commands;

namespace PetProject.Application.Volunteers.DeleteVolunteer;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
