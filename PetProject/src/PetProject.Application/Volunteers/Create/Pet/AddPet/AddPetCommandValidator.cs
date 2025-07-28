using FluentValidation;
using PetProject.Application.Commands;
using PetProject.Application.Validation;
using PetProject.Domain;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.Pet.AddPet;

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
