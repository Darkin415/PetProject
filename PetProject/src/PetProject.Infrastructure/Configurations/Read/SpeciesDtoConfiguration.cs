using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.title);
    }
}
