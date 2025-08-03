using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PetProject.SharedKernel.ValueObjects;

namespace PetProject.Species.Infrastructure;

public class SpeciesConfiguration : IEntityTypeConfiguration<Domain.PetSpecies.Species>
{
    public void Configure(EntityTypeBuilder<Domain.PetSpecies.Species> builder)
    {
        builder.ToTable("species");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
            id => id.Value,
            guid => SpeciesId.Create(guid).Value);
        
        builder.ComplexProperty(p => p.Title, g =>
        {
            g.Property(g => g.Name)
                .IsRequired()
                .HasColumnName("title");
        });
        
        builder.HasMany(p => p.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
