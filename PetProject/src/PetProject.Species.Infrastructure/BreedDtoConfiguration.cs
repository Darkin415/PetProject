using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;
using PetProject.Species.Contracts.DTOs;

namespace PetProject.Species.Infrastructure;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breed");
        builder.HasKey(p => p.Id);      
    }
}