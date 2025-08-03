using CSharpFunctionalExtensions;
using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts.DTOs;
using PetProject.Volunteers.Contracts.ids;
using PetProject.Volunteers.Contracts.Requests;

namespace PetProject.Volunteers.Contracts;

public interface IVolunteersContract
{
    Task<Guid> Add(CreateVolunteerRequest createVolunteerRequest, CancellationToken cancellationToken = default);
    
    Task<Result<Guid, ErrorList>> Delete(Guid id, CancellationToken cancellationToken);

    Task<Result<VolunteerDto, Error>> GetVolunteerById(Guid volunteerId,
        CancellationToken cancellationToken = default);
    
    
}