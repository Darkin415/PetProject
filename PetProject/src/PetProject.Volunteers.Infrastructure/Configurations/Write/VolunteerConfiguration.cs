using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Contracts.Ids;
using PetProject.Pets.Domain;

namespace PetProject.Volunteers.Infrastructure.Configurations.Write;
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
            .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.Property(b => b.FirstName)
            .IsRequired()
            .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("first_name");

            b.Property(b => b.LastName)
            .IsRequired()
            .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("last_name");

            b.Property(b => b.Surname)
            .IsRequired()
            .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH)
            .HasColumnName("surname");
        });

        builder.OwnsMany(v => v.Socials, sb =>
        {
            sb.ToJson("Social"); 

            sb.Property(s => s.LinkMedia)
                .IsRequired()
                .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH)
                .HasColumnName("link_media");

            sb.Property(s => s.Title)
                .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH)
                .HasColumnName("title");
        });

        
        builder.ComplexProperty(v => v.Email, b =>
        {
            b.Property(b => b.Link)
            .HasColumnName("link")
            .IsRequired()
            .HasMaxLength(Core.Constants.MIDDLE_TEXT_LENGTH);
        });

        builder.ComplexProperty(v => v.Description, b =>
        {
            b.Property(b => b.Information)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(Core.Constants.MAX_HIGH_TEXT_LENGTH);
        });
           
        
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(v => v.Pets).AutoInclude();

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
