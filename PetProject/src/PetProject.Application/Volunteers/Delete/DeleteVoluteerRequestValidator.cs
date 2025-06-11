using FluentValidation;

namespace PetProject.Application.Volunteers.Delete;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.Request.VolunteerId).NotEmpty();
    }
}
