using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
{
    public void Configure(EntityTypeBuilder<UserAchievement> builder)
    {
        builder.HasKey(ua => ua.ID);
        
        builder.Property(ua => ua.DateEarned)
            .IsRequired();
        
        builder.HasOne<User>(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Achievement>(ua => ua.Achievement)
            .WithMany()
            .HasForeignKey(ua => ua.AchievementID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}