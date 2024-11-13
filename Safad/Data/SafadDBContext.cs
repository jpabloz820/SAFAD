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
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<ConfigurationMetric> ConfigurationMetrics { get; set; }
        public DbSet<GoalIndicator> GoalIndicators { get; set; }

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
                tb.Property(col => col.CategoryId).IsRequired().ValueGeneratedNever(); ;
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
                tb.HasOne(t => t.Division)
                    .WithMany(d => d.Teams)
                    .HasForeignKey(t => t.DivisionId)
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
                    .WithMany(uc => uc.TeamUserAthletes)
                    .HasForeignKey(t => t.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                tb.HasOne(t => t.User_Athlete)
                    .WithMany(uc => uc.TeamUserAthletes)
                    .HasForeignKey(t => t.UserAthleteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Division>(tb =>
            {
                tb.HasKey(col => col.DivisionId);
                tb.Property(col => col.DivisionId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.DivisionName).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Description).HasMaxLength(255);
                tb.HasOne(d => d.Category)
                    .WithMany(c => c.Division)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Phase>().HasData(
                new Phase { PhaseId = 1, PhaseName = "Fase de Preparación Física" },
                new Phase { PhaseId = 2, PhaseName = "Fase Técnica y Táctica" },
                new Phase { PhaseId = 3, PhaseName = "Fase de Integración Táctica Colectiva" },
                new Phase { PhaseId = 4, PhaseName = "Fase de Competencia" },
                new Phase { PhaseId = 5, PhaseName = "Fase de Recuperación" },
                new Phase { PhaseId = 6, PhaseName = "Fase de Transición" },
                new Phase { PhaseId = 7, PhaseName = "Fase de Análisis de Rendimiento" }
            );
            modelBuilder.Entity<Metric>(tb =>
            {
                tb.HasKey(col => col.MetricId);
                tb.Property(col => col.MetricId).IsRequired().ValueGeneratedNever();
                tb.Property(col => col.MetricName).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Indicator).IsRequired();
                tb.Property(col => col.Measure).IsRequired().HasMaxLength(25);
            });
            modelBuilder.Entity<ConfigurationMetric>()
            .HasKey(pcm => new { pcm.PhaseId, pcm.MetricId, pcm.CategoryId });
            modelBuilder.Entity<ConfigurationMetric>()
                .HasOne(pcm => pcm.Phase)
                .WithMany(p => p.ConfigurationMetric)
                .HasForeignKey(pcm => pcm.PhaseId);
            modelBuilder.Entity<ConfigurationMetric>()
                .HasOne(pcm => pcm.Metric)
                .WithMany(m => m.ConfigurationMetric)
                .HasForeignKey(pcm => pcm.MetricId);
            modelBuilder.Entity<ConfigurationMetric>()
                .HasOne(pcm => pcm.Category)
                .WithMany(c => c.ConfigurationMetric)
                .HasForeignKey(pcm => pcm.CategoryId);
            modelBuilder.Entity<GoalIndicator>(tb =>
            {
                tb.HasKey(gi => gi.GoalIndicatorId);
                tb.Property(col => col.GoalIndicatorId).IsRequired().ValueGeneratedNever();
                tb.Property(gi => gi.MeasureAthlete).IsRequired();
                tb.HasOne(gi => gi.User_Athlete)
                    .WithMany(ua => ua.GoalIndicator)
                    .HasForeignKey(gi => gi.UserAthleteId)
                    .OnDelete(DeleteBehavior.Restrict);
                tb.HasOne(gi => gi.Metric)
                    .WithMany(m => m.GoalIndicator)
                    .HasForeignKey(gi => gi.MetricId)
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
            modelBuilder.Entity<Division>().ToTable("Divisions");
            modelBuilder.Entity<Phase>().ToTable("Phases");
            modelBuilder.Entity<Metric>().ToTable("Metrics");
            modelBuilder.Entity<ConfigurationMetric>().ToTable("ConfigurationMetrics");
            modelBuilder.Entity<GoalIndicator>().ToTable("GoalIndicators");
        }
    }
}