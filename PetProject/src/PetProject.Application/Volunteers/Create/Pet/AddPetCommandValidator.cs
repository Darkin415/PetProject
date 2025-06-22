using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObject;

namespace PetProject.Application.Volunteers.Create.Pet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.PhysicalAttribute).MustBeValueObject(p => PhysicalAttributes.Create(p.Weight, p.Height));
        RuleFor(a => a.OwnerTelephonNumber).MustBeValueObject(TelephonNumber.Create);
        RuleFor(a => a.BirthDate).MustBeValueObject(BirthDay.Create);       
        RuleFor(a => a.CastrationStatus).MustBeValueObject(CastrationStatus.Create);
        RuleFor(a => a.Color).MustBeValueObject(Color.Create);
        RuleFor(a => a.DateOfCreation).MustBeValueObject(DateOfCreation.Create);
        RuleFor(a => a.Photos).NotEmpty();
    }
}
