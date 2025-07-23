using CSharpFunctionalExtensions;
using PetProject.Application.Abstraction;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Species;

// public class CreateSpeciesHandler : ICommandHandler<Guid, CreateSpeciesCommand>
// {
//     private readonly ISpeciesRepository _speciesRepository;
//
//     public CreateSpeciesHandler(ISpeciesRepository speciesRepository)
//     {
//         _speciesRepository = speciesRepository;
//     }
//     public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken)
//     {
//         
//         var speciesResult = await _speciesRepository.CreateSpecies(command.Title, cancellationToken);
//         if (speciesResult.IsFailure)
//             return speciesResult.Error;
//
//         return speciesResult.Value;
//
//     }
// }