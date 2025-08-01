using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;
using PetProject.Volunteers.Contracts.DTOs;

namespace PetProject.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(p => p.Id);       

        builder.Property(v => v.Description).HasColumnName("description");        
    }
}
