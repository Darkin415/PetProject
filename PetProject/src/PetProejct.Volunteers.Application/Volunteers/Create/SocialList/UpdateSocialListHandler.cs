using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProejct.Volunteers.Application.Commands;
using PetProject.Contracts;
using PetProject.Contracts.Abstraction;
using PetProject.Contracts.Ids;
using PetProject.Volunteers.Domain.Pets;

namespace PetProejct.Volunteers.Application.Volunteers.Create.SocialList;

public class UpdateSocialListHandler : ICommandHandler<Guid, UpdateSocialNetworksCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateSocialListHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSocialListHandler(
        IVolunteersRepository volunteersRepository,       
        ILogger<UpdateSocialListHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
    UpdateSocialNetworksCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
             
        var socialMediaResults = command.SocialMedias
        .Select(dto => SocialMedia.Create(dto.Title, dto.LinkMedia))
        .ToList();
        if (socialMediaResults.Any(r => r.IsFailure))
            return Result.Failure<Guid, ErrorList>(socialMediaResults.First(r => r.IsFailure).Error.ToErrorList());

        var socialMediasList = socialMediaResults
    .Select(r => r.Value)
    .ToList();

        volunteerResult.Value.UpdateSocialList(socialMediasList);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer's social network has been updated with id {volunteerId}", volunteerId);

        return volunteerResult.Value.Id.Value;
    }
    
}