using FluentValidation;
using PetProject.Contracts.Validation;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain.PetValueObjects;

namespace PetProejct.Volunteers.Application.Commands.CreatePet;

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
