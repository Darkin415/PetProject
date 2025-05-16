using CSharpFunctionalExtensions;
using PetProject.Domain;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId);
        Task<Result<Volunteer, Error>> GetByEmail(Email email);
    }
}