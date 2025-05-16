using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain;
using PetProject.Domain.Shared;

namespace PetProject.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
            id => id.Value,
            guid => VolunteerId.Create(guid).Value);

        builder.ComplexProperty(v => v.TelephonNumber, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("telehon_number")
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.Property(b => b.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("first_name");

            b.Property(b => b.LastName)
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("last_name");

            b.Property(b => b.Surname)
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("surname");
        });

        builder.OwnsOne(v => v.SocialList, vb =>
        {
            vb.ToJson();
            vb.OwnsMany(d => d.SocialMedias, db =>
            {
                db.Property(f => f.LinkMedia)
                .IsRequired()
                .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);

                db.Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
            });
        });

        
        builder.ComplexProperty(v => v.Email, b =>
        {
            b.Property(b => b.Link)
            .HasColumnName("link")
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(v => v.Description, b =>
        {
            b.Property(b => b.Information)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        });
           
        
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();




    }
}
