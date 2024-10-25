using Microsoft.EntityFrameworkCore;
using Safad.Models;

namespace Safad.Data
{
    public class SafadDBContext : DbContext
    {
        public SafadDBContext(DbContextOptions<SafadDBContext> options) : base(options) { } // Ctor

        public DbSet<User> Users { get; set; } // prop
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCoach> UserCoaches { get; set; }
        public DbSet<User_Athlete> UserAthletes { get; set; }
        public DbSet<Profesional> Profesional { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TeamProfessional> TeamProfessionals { get; set; }
        public DbSet<TeamUserAthlete> TeamUserAthletes { get; set; }

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
                tb.Property(col => col.PhotoPath).IsRequired().HasMaxLength(255);
                tb.HasOne(uc => uc.User)
                    .WithMany() 
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                tb.HasOne(uc => uc.Team)
                    .WithOne(t => t.UserCoach)
                    .HasForeignKey<Team>(t => t.UserCoachId);
            });
            modelBuilder.Entity<User_Athlete>(tb =>
            {
                tb.HasKey(col => col.UserAthleteId);
                tb.Property(col => col.UserAthleteId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.NameAthlete).IsRequired().HasMaxLength(100);
                tb.Property(col => col.DniAthlete).IsRequired().HasMaxLength(20);
                tb.Property(col => col.Cellphone).HasMaxLength(15);
                tb.Property(col => col.Address).HasMaxLength(150);
                tb.Property(col => col.Age).IsRequired();
                tb.Property(col => col.Weight).IsRequired();
                tb.Property(col => col.Height).IsRequired();
                tb.Property(col => col.Position).HasMaxLength(150);
                tb.Property(col => col.PhotoPath).IsRequired().HasMaxLength(255);
                tb.HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Profesional>(tb =>
            {
                tb.HasKey(col => col.ProfesionalId);
                tb.Property(col => col.ProfesionalId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.NameProfesional).IsRequired().HasMaxLength(100);
                tb.Property(col => col.DniProfesional).IsRequired().HasMaxLength(20);
                tb.Property(col => col.Cellphone).HasMaxLength(15);
                tb.Property(col => col.Address).HasMaxLength(150);
                tb.HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Category>(tb =>
            {
                tb.HasKey(col => col.CategoryId);
                tb.Property(col => col.CategoryName).IsRequired().HasMaxLength(50);
                tb.Property(col => col.CategoryDescription).HasMaxLength(255);
            });
            modelBuilder.Entity<Team>(tb =>
            {
                tb.HasKey(col => col.TeamId);
                tb.Property(col => col.TeamId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.TeamName).IsRequired().HasMaxLength(100);
                tb.Property(col => col.TeamLogo).HasMaxLength(255);
                tb.Property(col => col.MaxPlayers).IsRequired();
                tb.Property(col => col.MinPlayers).IsRequired();
                tb.HasOne(t => t.UserCoach)
                    .WithOne(uc => uc.Team)
                    .HasForeignKey<Team>(t => t.UserCoachId)
                    .OnDelete(DeleteBehavior.Restrict);
                tb.HasOne(t => t.Category)
                    .WithMany(c => c.Teams)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<TeamProfessional>(tb =>
            {
                tb.HasKey(col => col.TeamProfessionalId);
                tb.Property(col => col.TeamProfessionalId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.TeamId).IsRequired();
                tb.Property(col => col.ProfesionalId).IsRequired();
                tb.HasOne(t => t.Team)
                    .WithOne(uc => uc.TeamProfessional)
                    .HasForeignKey<TeamProfessional>(t => t.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                tb.HasOne(t => t.Profesional)
                    .WithOne(uc => uc.TeamProfessional)
                    .HasForeignKey<TeamProfessional>(t => t.ProfesionalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<TeamUserAthlete>(tb =>
            {
                tb.HasKey(col => col.TeamUserAthleteId);
                tb.Property(col => col.TeamUserAthleteId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.TeamId).IsRequired();
                tb.Property(col => col.UserAthleteId).IsRequired();
                tb.Property(col => col.FootballNumber).IsRequired().HasMaxLength(25);
                tb.HasOne(t => t.Team)
                    .WithOne(uc => uc.TeamUserAthlete)
                    .HasForeignKey<TeamUserAthlete>(t => t.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                tb.HasOne(t => t.User_Athlete)
                    .WithOne(uc => uc.TeamUserAthlete)
                    .HasForeignKey<TeamUserAthlete>(t => t.UserAthleteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserCoach>().ToTable("UserCoaches");
            modelBuilder.Entity<User_Athlete>().ToTable("UserAthletes");
            modelBuilder.Entity<Profesional>().ToTable("Profesional");
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<TeamProfessional>().ToTable("TeamProfessionals");
            modelBuilder.Entity<TeamUserAthlete>().ToTable("TeamUserAthletes");
        }
    }
}