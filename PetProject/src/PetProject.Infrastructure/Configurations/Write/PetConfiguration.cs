using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Domain.PetSpecies;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Ids;
using PetProject.Domain.Volunteers;
using System.Text.Json;
using static PetProject.Contracts.Dtos.PetsDto;
using static PetProject.Domain.Volunteers.Pet;

namespace PetProject.Infrastructure.Configurations.Write;

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


        builder.ComplexProperty(p => p.Position, g =>
        {
            g.Property(g => g.Value)
            .HasColumnName("serial_number")
            .IsRequired(true);
        });


        builder.ComplexProperty(p => p.Nickname, g =>
        {
            g.Property(g => g.Name)
            .HasColumnName("nick_name")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
        });             

        builder.ComplexProperty(p => p.Color, g =>
        {
            g.Property(g => g.Value)
            .HasColumnName("color")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Height, g =>
        {
            g.Property(g => g.Value)
                .HasColumnName("height")
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
        });
        
        builder.ComplexProperty(p => p.Weight, g =>
        {
            g.Property(g => g.Value)
                .HasColumnName("weight")
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.StatusHealth, g =>
        {
            g.Property(g => g.Value)
             .HasColumnName("status_health")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
        });

        
        builder.ComplexProperty(P => P.OwnerTelephoneNumber, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("owner_telephon_number")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(P => P.CastrationStatus, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("castration_status")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(P => P.VaccinationStatus, b =>
        {
            b.Property(b => b.Value)
            .HasColumnName("vaccination_status")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MIDDLE_TEXT_LENGTH);
        });


        builder.ComplexProperty(p => p.BirthDate, b =>
        {
            b.Property(b => b.BirthDate)
            .HasColumnName("birth_date")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);


        builder.ComplexProperty(p => p.DateOfCreation, b =>
        {
            b.Property(b => b.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MIDDLE_TEXT_LENGTH);
        });


        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.Property(p => p.Photos)
            .HasColumnType("jsonb")
            .HasConversion(
            photos => JsonSerializer.Serialize(
                photos.Select(p => new PetFileDto
                {
                    PathToStorage = p.PathToStorage.Path
                }),
                JsonSerializerOptions.Default),


            json => JsonSerializer.Deserialize<List<PetFileDto>>(json, JsonSerializerOptions.Default)!
            .Select(p =>
            new Photos(FilePath.Create(p.PathToStorage).Value))
            .ToList(),

            new ValueComparer<IReadOnlyList<Photos>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => (IReadOnlyList<Photos>)c.ToList()));

        builder.ComplexProperty(p => p.PetInfo, pi =>
        {
            pi.Property(x => x.SpeciesId)
            .HasColumnName("SpeciesId")
            .HasConversion(
                id => id.Value,
                guid => SpeciesId.Create(guid).Value
                );
            pi.Property(x => x.BreedId)
            .HasColumnName("BreedId")
            .HasConversion(
                id => id.Value,
                guid => BreedId.Create(guid).Value);
        });
    }
}
