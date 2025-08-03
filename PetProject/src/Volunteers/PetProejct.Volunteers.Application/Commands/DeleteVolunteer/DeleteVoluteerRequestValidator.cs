using FluentValidation;

namespace PetProejct.Volunteers.Application.Commands.DeleteVolunteer;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
