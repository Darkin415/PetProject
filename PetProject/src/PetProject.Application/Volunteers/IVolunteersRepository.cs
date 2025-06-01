using CSharpFunctionalExtensions;
using PetProject.Domain;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default);
        Task<Guid> Update(Volunteer volunteer, CancellationToken cancellationToken = default);
    }
}