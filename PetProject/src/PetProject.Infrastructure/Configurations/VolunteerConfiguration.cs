using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetProject.Domain.Volunteer;
using PetProject.Domain.Shared;

namespace PetProject.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteer_id");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));
            builder.ComplexProperty(b => b.FullName, eb =>
            {
                eb.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("first_name");

            });
            builder.Property(v => v.Email)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            builder.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_MIDDLE_LENGTH);
            builder.HasMany(p => p.Pets)
                .WithOne()
                .HasForeignKey("VolunteerId");
            builder.OwnsOne(v => v.SocialList, b =>
            {
                b.ToJson();
                b.OwnsMany(b => b.SocialMedias, ib =>
                {
                    ib.Property(i => i.LinkMedia)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                });
            });
           
            builder.ComplexProperty(b => b.PhoneNumber, eb =>
            {
                eb.Property(e => e.TelephoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("telephon_number");

            });




        }
    }
}
