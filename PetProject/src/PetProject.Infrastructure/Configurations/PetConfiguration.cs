using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
            id => id.Value,
            guid => PetId.Create(guid).Value);

        builder.ComplexProperty(p => p.Nickname, g =>
        {
            g.Property(g => g.Name)
             .HasColumnName("nick_name")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.View, g =>
        {
            g.Property(g => g.Value)
             .HasColumnName("View")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Breed, g =>
        {
            g.Property(g => g.Name)
             .HasColumnName("breed")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Color, g =>
        {
            g.Property(g => g.Value)
             .HasColumnName("color")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.StatusHealth, g =>
        {
            g.Property(g => g.Value)
             .HasColumnName("status_health")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Attributes, g =>
        {
            g.Property(g => g.Weight)
             .HasColumnName("attributes")
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            g.Property(g => g.Height)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(P => P.OwnerTelephoneNumber, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("owner_telephon_number")
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(P => P.CastrationStatus, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("castration_status")
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(P => P.VaccinationStatus, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("vaccination_status")
            .IsRequired()
            .HasMaxLength(Constants.MIDDLE_TEXT_LENGTH);
        });


        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

        
        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

        builder.Property(p => p.DateOfCreation)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
