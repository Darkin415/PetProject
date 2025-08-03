
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PetProject.SharedKernel.ValueObjects;
using PetProject.Species.Domain.PetSpecies;

namespace PetProject.Species.Infrastructure;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
            id => id.Value,
            value => BreedId.Create(value).Value);
        
        builder.Property(p => p.SpeciesId)
            .HasConversion(
                id => id.Value,
                guid => SpeciesId.Create(guid).Value);

        builder.ComplexProperty(p => p.Title, g =>
        {
            g.Property(g => g.Name)
                .IsRequired()
                .HasColumnName("title");
        });
    }
}
