using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class UserCategoryConfiguration : IEntityTypeConfiguration<UserCategory>
{
    public void Configure(EntityTypeBuilder<UserCategory> builder)
    {
        builder.HasKey(uc => uc.ID);

        builder.Property(uc => uc.Title)
            .IsRequired();
        
        builder.HasOne<User>(uc => uc.User)
            .WithMany()
            .HasForeignKey(uc => uc.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<PlantType>(uc => uc.PlantType)
            .WithMany()
            .HasForeignKey(uc => uc.PlantTypeID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}