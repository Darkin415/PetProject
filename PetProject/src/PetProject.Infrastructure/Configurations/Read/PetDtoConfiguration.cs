using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PetProject.Contracts.Dtos.PetDto;

namespace PetProject.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Photos)
            .HasConversion(
            photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),

            json => JsonSerializer.Deserialize<PetFileDto[]>(json, JsonSerializerOptions.Default)!);
            
    }
}
