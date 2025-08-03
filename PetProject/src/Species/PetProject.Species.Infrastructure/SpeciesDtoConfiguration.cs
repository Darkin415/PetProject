using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Species.Contracts.DTOs;

namespace PetProject.Species.Infrastructure;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.title);
    }
}
