using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.PetSpecies;

namespace PetProject.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
            id => id.Value,
            guid => BreedId.Create(guid).Value);
        
        // builder.ComplexProperty(p => p.Title, g =>
        // {
        //     g.Property(g => g.Name)
        //         .IsRequired()
        //         .HasColumnName("title");
        // });
    }
}
