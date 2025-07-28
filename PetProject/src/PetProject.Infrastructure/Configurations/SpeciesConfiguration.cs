using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.PetSpecies;

namespace PetProject.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
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
    }
}
