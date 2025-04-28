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
            builder.Property(v => v.fullName)
                .IsRequired()
                .HasMaxLength(Constants.VERY_LOW_LENGTH);
            builder.Property(v => v.Email)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            builder.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_MIDDLE_LENGTH);
            builder.HasMany(p => p.Pets)
                .WithOne()
                .HasForeignKey("VolunteerId");
            builder.OwnsOne(v => v.SocialMedias, js =>
            {
                js.ToJson();

                js.OwnsMany(x => x, ig =>
                {
                    ig.Property(sm => sm.Title)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    ig.Property(sm => sm.LinkMedia)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });
            builder.Property(s => s.fullName)
                .IsRequired()
                .HasMaxLength(Constants.VERY_LOW_LENGTH);
            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.VERY_LOW_LENGTH);




        }
    }
}
