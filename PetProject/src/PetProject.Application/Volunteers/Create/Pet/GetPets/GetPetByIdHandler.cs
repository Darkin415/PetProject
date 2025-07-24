using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetProject.Application.Database;
using PetProject.Application.Volunteers.Queries;
using PetProject.Contracts.Dtos;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Volunteers.Create.Pet.GetPets;

public class GetPetByIdHandler
{
    public readonly IReadDbContext _readDbContext;

    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PetDto, Error>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var petDto = await _readDbContext.Pets
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
