﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Contracts.Commands;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Shared.ValueObjects;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.Create.SocialList;

public class UpdateSocialListHandler
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
    public async Task<Result<Guid, Error>> Handle(
    UpdateSocialNetworksCommand command,
    CancellationToken cancellationToken = default)
    {

        var volunteerId = new VolunteerId(command.VolunteerId);

        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
             
        var socialMediaResults = command.SocialMedias
        .Select(dto => SocialMedia.Create(dto.Title, dto.LinkMedia))
        .ToList();
        if (socialMediaResults.Any(r => r.IsFailure))
            return Result.Failure<Guid, Error>(socialMediaResults.First(r => r.IsFailure).Error);

        var socialMediasList = socialMediaResults
    .Select(r => r.Value)
    .ToList();

        volunteerResult.Value.UpdateSocialList(socialMediasList);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer's social network has been updated with id {volunteerId}", volunteerId);

        return volunteerResult.Value.Id.Value;
    }
}