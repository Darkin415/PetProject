using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

using PetProject.SharedKernel;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetById;

public class GetPetByIdHandler
{
    public readonly IVolunteersReadDbContext VolunteersReadDbContext;

    public GetPetByIdHandler(IVolunteersReadDbContext volunteersReadDbContext)
    {
        VolunteersReadDbContext = volunteersReadDbContext;
    }

    public async Task<Result<PetDto, Error>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var petDto = await VolunteersReadDbContext.Pets
            .AsNoTracking()
            .Where(v => v.Id == query.Id)
            .Select(v => new PetDto
            {
                Id = v.Id,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (petDto == null)
            return Error.NotFound("pet_not_found", "Pet not found");

        return petDto;
    }
}
