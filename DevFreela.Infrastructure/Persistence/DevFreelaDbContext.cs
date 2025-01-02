using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Skill>(e =>
                {
                    e.HasKey(s => s.Id);
                });

            builder
                .Entity<UserSkill>(e =>
                {
                    e.HasKey(us => us.Id);

                    e.HasOne(us => us.User)
                        .WithMany(u => u.Skills)
                        .HasForeignKey(us => us.IdUser)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(s => s.Skill)
                        .WithMany(us => us.UserSkills)
                        .HasForeignKey(us => us.IdSkill)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<ProjectComment>(e =>
                {
                    e.HasKey(pc => pc.Id);

                    e.HasOne(p => p.Project)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(pc => pc.IdProject)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(pc => pc.User)
                        .WithMany(u => u.Comments)
                        .HasForeignKey(pc => pc.IdUser)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<User>(e =>
                {
                    e.HasKey(u => u.Id);
                });

            builder
                .Entity<Project>(e =>
                {
                    e.HasKey(p => p.Id);

                    e.HasOne(p => p.Freelancer)
                        .WithMany(f => f.FreelanceProjects)
                        .HasForeignKey(p => p.IdFreelancer)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(p => p.Client)
                        .WithMany(c => c.OwnedProjects)
                        .HasForeignKey(p => p.IdClient)
                        .OnDelete(DeleteBehavior.Restrict);

                });

            base.OnModelCreating(builder);
        }
    }
}