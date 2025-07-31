using FluentValidation;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Contracts.Validation;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain.Pets;

namespace PetProejct.Volunteers.Application.Pet.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        
        RuleFor(a => a.OwnerTelephonNumber).MustBeValueObject(TelephonNumber.Create);
        RuleFor(a => a.BirthDate).MustBeValueObject(BirthDay.Create);       
        RuleFor(a => a.CastrationStatus).MustBeValueObject(CastrationStatus.Create);
        RuleFor(a => a.Color).MustBeValueObject(Color.Create);
        RuleFor(a => a.DateOfCreation).MustBeValueObject(DateOfCreation.Create);
    }
}
