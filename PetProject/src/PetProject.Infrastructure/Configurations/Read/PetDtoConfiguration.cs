using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Application.Volunteers.Create.Pet.GetPets;
using PetProject.Contracts.Dtos;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PetProject.Application.Volunteers.Create.Pet.GetPets.PetsDto;


namespace PetProject.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetsDto>
{
    public void Configure(EntityTypeBuilder<PetsDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nickname).HasColumnName("nick_name");

        builder.Property(p => p.VolunteerId).HasColumnName("volunteer_id");

        builder.Property(p => p.Color).HasColumnName("color");

        builder.Property(p => p.CastrationStatus).HasColumnName("castration_status");   

        builder.Property(p => p.VaccinationStatus).HasColumnName("vaccination_status");       

        builder.Property(p => p.Photos)
            .HasConversion(
            photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),

            json => JsonSerializer.Deserialize<PetFileDto[]>(json, JsonSerializerOptions.Default)!);
            
    }
}
