using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.Contracts.Ids;
using PetProject.Contracts.ValueObjects;
using PetProject.Volunteers.Domain;

namespace PetProejct.Volunteers.Application;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Guid Save(Volunteer volunteer, CancellationToken cancellationToken);

    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken);

    Result<PetProject.Volunteers.Domain.Pets.Pet, Error> GetByPetId(PetId petId);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);      
}