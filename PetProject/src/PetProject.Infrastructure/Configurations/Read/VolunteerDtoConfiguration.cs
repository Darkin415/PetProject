using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Dtos;

namespace PetProject.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(p => p.Id);

        builder.HasMany(i => i.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);

        builder.Property(v => v.Description).HasColumnName("description");        
    }
}
