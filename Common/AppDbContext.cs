using Microsoft.EntityFrameworkCore;
using StudyGarden.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Task = StudyGarden.Entities.Task;

namespace StudyGarden.Common;

public class AppDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Task> Task { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Plant> Plant { get; set; }
    public DbSet<PlantType> PlantType { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }
    public DbSet<Friend> Friend { get; set; }
    public DbSet<Garden> Garden { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.ID);

        modelBuilder.Entity<Task>()
            .HasKey(t => t.ID);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.ID);

        modelBuilder.Entity<PlantType>()
            .HasKey(pt => pt.ID);

        modelBuilder.Entity<Friend>()
            .HasKey(fr => fr.ID);

        modelBuilder.Entity<Achievement>()
            .HasKey(a => a.ID);

        modelBuilder.Entity<Plant>()
            .HasKey(p => p.ID);

        modelBuilder.Entity<Garden>()
            .HasKey(g => g.ID);
    }
}