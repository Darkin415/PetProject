using FluentValidation;
using PetProejct.Volunteers.Application.Commands;

namespace PetProejct.Volunteers.Application.Volunteers.DeleteVolunteer;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
