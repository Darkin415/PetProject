using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
    Result<Pet, Error> GetByPetId(PetId petId);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);      
}