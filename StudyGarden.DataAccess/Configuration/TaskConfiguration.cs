using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.DataAccess.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.ID);

        builder.Property(t => t.Title)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired();
        
        builder.Property(t => t.CreatedDate)
            .IsRequired();

        builder.HasOne<User>(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Plant>(t => t.Plant) 
            .WithMany()
            .HasForeignKey(t => t.PlantID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<UserCategory>(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}