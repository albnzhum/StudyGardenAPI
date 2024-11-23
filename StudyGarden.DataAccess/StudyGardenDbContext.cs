using Microsoft.EntityFrameworkCore;
using StudyGarden.Core.Models;
using Task = StudyGarden.Core.Models.Task;

namespace StudyGarden.DataAccess;

public class StudyGardenDbContext(DbContextOptions<StudyGardenDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Task> Tasks { get; private set; }
    public DbSet<Achievement> Achievements { get; private set; }
    public DbSet<UserCategory> UserCategory { get; private set; }
    public DbSet<Plant> Plants { get; private set; }
    public DbSet<PlantType> PlantsType { get; private set; }
    public DbSet<UserAchievement> UserAchievements { get; private set; }
    public DbSet<Friend> Friends { get; private set; }
    public DbSet<Garden> Garden { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.ID);

        modelBuilder.Entity<Task>()
            .HasKey(t => t.ID);

        modelBuilder.Entity<UserCategory>()
            .HasKey(c => c.ID);

        modelBuilder.Entity<PlantType>()
            .HasKey(pt => pt.ID);

        modelBuilder.Entity<Friend>()
            .HasKey(fr => fr.ID);
        
        modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany() // Specify the relationship direction if needed (e.g., `.WithMany(u => u.Friends)`).
            .HasForeignKey(f => f.UserID)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<Friend>()
            .HasOne(f => f.FriendFK)
            .WithMany() // Specify the relationship direction if needed
            .HasForeignKey(f => f.FriendID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Achievement>()
            .HasKey(a => a.ID);

        modelBuilder.Entity<Plant>()
            .HasKey(p => p.ID);

        modelBuilder.Entity<Garden>()
            .HasKey(g => g.ID);
    }
}