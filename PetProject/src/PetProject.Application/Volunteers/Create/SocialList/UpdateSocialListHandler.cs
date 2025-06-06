using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProject.Application.Volunteers.UpdateMainInfo;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObject;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Application.Volunteers.Create.SocialList;
public record UpdateSocialListCommand(UpdateSocialListRequest Request);
public class UpdateSocialListHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateSocialListHandler> _logger;
    public UpdateSocialListHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateSocialListHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Handle(
    UpdateSocialListCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.Request.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var socialMediaResult = command.Request.SocialMedias
    .Select(x => SocialMedia.Create(x.Title, x.LinkMedia))
    .ToList();

        var validSocialMedias = socialMediaResult
    .Where(r => r.IsSuccess)
    .Select(r => r.Value)
    .ToList();

        var socialMediaList = new SocialMediaList(validSocialMedias);

        volunteerResult.Value.UpdateSocialList(socialMediaList);

        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Volunteer's social network has been updated with id {volunteerId}", volunteerId);

        return result;
    }
}