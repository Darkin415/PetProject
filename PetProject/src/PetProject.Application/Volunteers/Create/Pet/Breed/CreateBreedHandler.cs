// using CSharpFunctionalExtensions;
// using PetProject.Application.Abstraction;
// using PetProject.Domain.PetSpecies;
// using PetProject.Domain.Shared.ValueObjects;
//
// namespace PetProject.Application.Volunteers.Create.Pet.Breed;
//
// public class CreateBreedHandler : ICommandHandler<Guid, CreateBreedCommand>
// {
//     private readonly ISpeciesRepository _speciesRepository;
//
//     public CreateBreedHandler(ISpeciesRepository speciesRepository)
//     {
//         _speciesRepository = speciesRepository;
//     }
//     public async Task<Result<Guid, ErrorList>> Handle(CreateBreedCommand command, CancellationToken cancellationToken)
//     {
//         var breedId = BreedId.NewBreedId();
//         
//         var breedResult = await _speciesRepository.CreateBreed(command.Title, cancellationToken);
//         if (breedResult.IsFailure)
//             return breedResult.Error;
//
//         return breedResult.Value;
//         
//     }
// }