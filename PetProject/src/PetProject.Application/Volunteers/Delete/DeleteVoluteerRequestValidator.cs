using FluentValidation;
using static PetProject.Application.Volunteers.Delete.DeleteVolunteerHandler;

namespace PetProject.Application.Volunteers.Delete;

public class DeleteVoluteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVoluteerCommandValidator()
    {
        RuleFor(d => d.Request.VolunteerId).NotEmpty();
    }
}
