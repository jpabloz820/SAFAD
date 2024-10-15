using Microsoft.EntityFrameworkCore;
using Safad.Models;

namespace Safad.Data
{
    public class SafadDBContext : DbContext
    {
        public SafadDBContext(DbContextOptions<SafadDBContext> options) : base(options) // Ctor
        {

        }

        public DbSet<User> Users { get; set; } // prop
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCoach> UserCoaches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // override OnMod
        {
            modelBuilder.Entity<User>(tb =>
            {
                tb.HasKey(col => col.UserId);
                tb.Property(col => col.UserId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.UserEmail).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Password).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Registration).IsRequired();
                tb.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Role>(tb =>
            {
                tb.HasKey(col => col.RoleId);
                tb.Property(col => col.RoleId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.RoleName).IsRequired().HasMaxLength(25);
            });
            modelBuilder.Entity<UserCoach>(tb =>
            {
                tb.HasKey(col => col.UserCoachId);
                tb.Property(col => col.UserCoachId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.NameCoach).IsRequired().HasMaxLength(100);
                tb.Property(col => col.DniCoach).IsRequired().HasMaxLength(20);
                tb.Property(col => col.Cellphone).HasMaxLength(15);
                tb.Property(col => col.Address).HasMaxLength(150);
                tb.HasOne(uc => uc.User)
                    .WithMany() 
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserCoach>().ToTable("UserCoaches");
        }
    }
}