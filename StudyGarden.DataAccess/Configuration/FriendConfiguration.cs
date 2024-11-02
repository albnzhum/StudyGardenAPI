using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyGarden.Core.Models;

namespace StudyGarden.DataAccess.Configuration;

public class FriendConfiguration :IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.HasKey(f => f.ID);
        
        builder.HasOne<User>(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>(f => f.FriendFK)
            .WithMany()
            .HasForeignKey(f => f.FriendID)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}