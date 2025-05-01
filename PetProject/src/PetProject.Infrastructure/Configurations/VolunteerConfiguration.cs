using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain;
using PetProject.Domain.Shared;

namespace PetProject.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));
            builder.ComplexProperty(v => v.TelephoneNumber, b =>
            {
                b.Property(b => b.NumberPhone)
                .IsRequired()
                .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
            });
                
                


        }
    }
}
