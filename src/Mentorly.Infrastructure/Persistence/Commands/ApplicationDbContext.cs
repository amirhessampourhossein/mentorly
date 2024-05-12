using Mentorly.Domain.Bookings;
using Mentorly.Domain.Common;
using Mentorly.Domain.Sessions;
using Mentorly.Domain.Transactions;
using Mentorly.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentorly.Persistence.Commands;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Session> Sessions => Set<Session>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<Expertise> Expertise => Set<Expertise>();

    public DbSet<Discipline> Disciplines => Set<Discipline>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<Tool> Tools => Set<Tool>();

    public DbSet<UserExpertise> UserExpertise => Set<UserExpertise>();

    public DbSet<UserDiscipline> UserDisciplines => Set<UserDiscipline>();

    public DbSet<UserSkill> UserSkills => Set<UserSkill>();

    public DbSet<UserTool> UserTools => Set<UserTool>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedDate = DateTime.UtcNow;

            entry.Entity.UpdatedDate = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedDate = DateTime.UtcNow;

            entry.Entity.UpdatedDate = DateTime.UtcNow;
        }

        return base.SaveChanges();
    }
}
