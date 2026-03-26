using Microsoft.EntityFrameworkCore;
using System;
using IssueTrackerAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Open" },
            new Status { Id = 2, Name = "In Progress" },
            new Status { Id = 3, Name = "Done" },
            new Status { Id = 4, Name = "Blocked" },
            new Status { Id = 5, Name = "Ready For QA" },
            new Status { Id = 6, Name = "Ready For Review" }
            );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "Nikhil" },
            new User { Id = 2, Username = "Govind" }
            );

        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Status>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<WorkItem>().HasQueryFilter(w => !w.IsDeleted);

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedOnAdd();

            entity.Property(e => e.event_time)
                  .HasDefaultValueSql("GETUTCDATE()");
        });
    }

}
