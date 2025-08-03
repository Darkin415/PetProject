using CSharpFunctionalExtensions;
using PetProejct.Volunteers.Application.Commands.CreateVolunteer;
using PetProejct.Volunteers.Application.Commands.DeleteVolunteer;
using PetProejct.Volunteers.Application.Commands.GetVolunteerById;
using PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;
using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts;
using PetProject.Volunteers.Contracts.DTOs;
using PetProject.Volunteers.Contracts.ids;
using PetProject.Volunteers.Contracts.Requests;

namespace PetProject.Volunteers.Presentation;

public class VolunteersContract(
    CreateVolunteerHandler createVolunteerHandler, 
    DeleteVolunteerHandler deleteVolunteerHandler,
    GetVolunteerByIdHandler getVolunteerByIdHandler) : IVolunteersContract
{
    public async Task<Guid> Add(CreateVolunteerRequest createVolunteerRequest, CancellationToken cancellationToken = default)
    {
        var command = new AddVolunteerCommand(
            createVolunteerRequest.Title,
            createVolunteerRequest.LinkMedia,
            createVolunteerRequest.Information,
            createVolunteerRequest.Email,
            createVolunteerRequest.PhoneNumber,
            createVolunteerRequest.FullName,
            createVolunteerRequest.SocialMedias);
        
        var result = await createVolunteerHandler.Handle(command, cancellationToken);
        
        return result.Value;
    }

    public Task<Result<Guid, ErrorList>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteVolunteerCommand(id);
        
        var result = deleteVolunteerHandler.Handle(command, cancellationToken);
        
        return result;
    }

    public Task<Result<VolunteerDto, Error>> GetVolunteerById(Guid volunteerId, CancellationToken cancellationToken = default)
    {
        var command = new GetVolunteerByIdQuery(volunteerId);
        
        var result = getVolunteerByIdHandler.Handle(command, cancellationToken);

        return result;
    }
    
}