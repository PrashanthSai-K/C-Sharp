using System;
using Microsoft.EntityFrameworkCore;
using TaskManagement_RESTAPI.Config.Migrations;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.AppDataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
                    .HasMany(u => u.TaskItems)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId);
        modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new TaskItemConfig());
    }
}
