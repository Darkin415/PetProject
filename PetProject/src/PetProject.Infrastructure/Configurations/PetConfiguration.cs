using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using PetProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => PetId.Create(value));
            builder.Property(p => p.Nickname)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
            builder.OwnsOne(p => p.Information, pi =>
            {
                pi.ToTable("pet_information");
                pi.WithOwner().HasForeignKey("PetId");
                pi.Property(pi => pi.SpeciesId)
                .IsRequired();
                pi.Property(pi => pi.BreedId)
                .IsRequired();               
            });
            builder.Property(p => p.View)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();
            builder.Property(p => p.StatusHealth)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(p => p.Weight)
                .IsRequired();
            builder.Property(p => p.Height)
                .IsRequired();
            builder.Property(p => p.CastrationStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(10);
            builder.Property(p => p.BirthDate)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(p => p.VaccinationStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(10);
            builder.Property(p => p.DateOfCreation)
               .IsRequired();



        }
    }
}
