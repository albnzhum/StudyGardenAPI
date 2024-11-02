using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.HasKey(a => a.ID);

        builder.Property(a => a.Title)
            .IsRequired();

        builder.Property(a => a.PlantID)
            .IsRequired();

        builder.HasOne(a => a.Plant)
            .WithMany()                   
            .HasForeignKey(a => a.PlantID)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}