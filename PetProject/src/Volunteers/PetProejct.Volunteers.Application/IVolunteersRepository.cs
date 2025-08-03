using CSharpFunctionalExtensions;
using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts.ids;
using PetProject.Volunteers.Domain;
using PetProject.Volunteers.Domain.Entities;
using PetProject.Volunteers.Domain.VolunteerValueObject;

namespace PetProejct.Volunteers.Application;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    // Guid Save(Volunteer volunteer, CancellationToken cancellationToken);

    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken);

    Result<Pet, Error> GetByPetId(PetId petId);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);      
}