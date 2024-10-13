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

        protected override void OnModelCreating(ModelBuilder modelBuilder) // override OnMod
        {
            modelBuilder.Entity<User>(tb =>
            {
                tb.HasKey(col => col.UserId);
                tb.Property(col => col.UserId).IsRequired();
                tb.Property(col => col.UserEmail).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Password).IsRequired().HasMaxLength(50);
                tb.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Role>(tb =>
            {
                tb.HasKey(col => col.RoleId);
                tb.Property(col => col.RoleId).IsRequired();
                tb.Property(col => col.RoleName).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
        }
    }
}