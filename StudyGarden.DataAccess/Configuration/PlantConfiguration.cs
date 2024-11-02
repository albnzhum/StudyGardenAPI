using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class PlantConfiguration :IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
        builder.HasKey(p => p.ID);

        builder.Property(p => p.Name)
            .IsRequired();

        builder.HasOne<PlantType>(p => p.PlantType)
            .WithMany()
            .HasForeignKey(p => p.PlantTypeID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}