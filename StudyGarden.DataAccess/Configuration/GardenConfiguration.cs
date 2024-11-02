using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class GardenConfiguration :IEntityTypeConfiguration<Garden>
{
    public void Configure(EntityTypeBuilder<Garden> builder)
    {
        builder.HasKey(g => g.ID);

        builder.Property(g => g.GrowthStage)
            .IsRequired();

        builder.Property(g => g.PositionX)
            .IsRequired();

        builder.Property(g => g.PositionY)
            .IsRequired();

        builder.Property(g => g.PositionZ)
            .IsRequired();
        
        builder.HasOne<User>(g => g.User)
            .WithMany()
            .HasForeignKey(g => g.UserID)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}