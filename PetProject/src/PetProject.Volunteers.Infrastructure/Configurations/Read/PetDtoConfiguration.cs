using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;


namespace PetProject.Volunteers.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetsDto>
{
    public void Configure(EntityTypeBuilder<PetsDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nickname).HasColumnName("nick_name");

        builder.Property(p => p.Height).HasColumnName("height");

        builder.Property(p => p.Weight).HasColumnName("weight");

        builder.Property(p => p.VolunteerId).HasColumnName("volunteer_id");

        builder.Property(p => p.Color).HasColumnName("color");

        builder.Property(p => p.CastrationStatus).HasColumnName("castration_status");   

        builder.Property(p => p.VaccinationStatus).HasColumnName("vaccination_status");       

        builder.Property(p => p.Photos)
            .HasConversion(
            photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),

            json => JsonSerializer.Deserialize<PetsDto.PetFileDto[]>(json, JsonSerializerOptions.Default)!);
            
    }
}
