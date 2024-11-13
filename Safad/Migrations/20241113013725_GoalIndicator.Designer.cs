﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Safad.Data;

#nullable disable

namespace Safad.Migrations
{
    [DbContext(typeof(SafadDBContext))]
    [Migration("20241113013725_GoalIndicator")]
    partial class GoalIndicator
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Safad.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Safad.Models.ConfigurationMetric", b =>
                {
                    b.Property<int>("PhaseId")
                        .HasColumnType("int");

                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("PhaseId", "MetricId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MetricId");

                    b.ToTable("ConfigurationMetrics", (string)null);
                });

            modelBuilder.Entity("Safad.Models.Division", b =>
                {
                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DivisionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DivisionId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Divisions", (string)null);
                });

            modelBuilder.Entity("Safad.Models.GoalIndicator", b =>
                {
                    b.Property<int>("GoalIndicatorId")
                        .HasColumnType("int");

                    b.Property<float>("MeasureAthlete")
                        .HasColumnType("real");

                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("UserAthleteId")
                        .HasColumnType("int");

                    b.HasKey("GoalIndicatorId");

                    b.HasIndex("MetricId");

                    b.HasIndex("UserAthleteId");

                    b.ToTable("GoalIndicators", (string)null);
                });

            modelBuilder.Entity("Safad.Models.Metric", b =>
                {
                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<float>("Indicator")
                        .HasColumnType("real");

                    b.Property<string>("Measure")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("MetricName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MetricId");

                    b.ToTable("Metrics", (string)null);
                });

            modelBuilder.Entity("Safad.Models.Phase", b =>
                {
                    b.Property<int>("PhaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhaseId"));

                    b.Property<string>("PhaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhaseId");

                    b.ToTable("Phases", (string)null);

                    b.HasData(
                        new
                        {
                            PhaseId = 1,
                            PhaseName = "Fase de Preparación Física"
                        },
                        new
                        {
                            PhaseId = 2,
                            PhaseName = "Fase Técnica y Táctica"
                        },
                        new
                        {
                            PhaseId = 3,
                            PhaseName = "Fase de Integración Táctica Colectiva"
                        },
                        new
                        {
                            PhaseId = 4,
                            PhaseName = "Fase de Competencia"
                        },
                        new
                        {
                            PhaseId = 5,
                            PhaseName = "Fase de Recuperación"
                        },
                        new
                        {
                            PhaseId = 6,
                            PhaseName = "Fase de Transición"
                        },
                        new
                        {
                            PhaseId = 7,
                            PhaseName = "Fase de Análisis de Rendimiento"
                        });
                });

            modelBuilder.Entity("Safad.Models.Profesional", b =>
                {
                    b.Property<int>("ProfesionalId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Cellphone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("DniProfesional")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NameProfesional")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProfesionalId");

                    b.HasIndex("UserId");

                    b.ToTable("Profesional", (string)null);
                });

            modelBuilder.Entity("Safad.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Safad.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayers")
                        .HasColumnType("int");

                    b.Property<string>("TeamLogo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserCoachId")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("UserCoachId")
                        .IsUnique();

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("Safad.Models.TeamProfessional", b =>
                {
                    b.Property<int>("TeamProfessionalId")
                        .HasColumnType("int");

                    b.Property<int>("ProfesionalId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("TeamProfessionalId");

                    b.HasIndex("ProfesionalId")
                        .IsUnique();

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("TeamProfessionals", (string)null);
                });

            modelBuilder.Entity("Safad.Models.TeamUserAthlete", b =>
                {
                    b.Property<int>("TeamUserAthleteId")
                        .HasColumnType("int");

                    b.Property<string>("FootballNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("UserAthleteId")
                        .HasColumnType("int");

                    b.HasKey("TeamUserAthleteId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserAthleteId");

                    b.ToTable("TeamUserAthletes", (string)null);
                });

            modelBuilder.Entity("Safad.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Registration")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Safad.Models.UserCoach", b =>
                {
                    b.Property<int>("UserCoachId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Cellphone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("DniCoach")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NameCoach")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserCoachId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCoaches", (string)null);
                });

            modelBuilder.Entity("Safad.Models.User_Athlete", b =>
                {
                    b.Property<int>("UserAthleteId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Cellphone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("DniAthlete")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("NameAthlete")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("UserAthleteId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAthletes", (string)null);
                });

            modelBuilder.Entity("Safad.Models.ConfigurationMetric", b =>
                {
                    b.HasOne("Safad.Models.Category", "Category")
                        .WithMany("ConfigurationMetric")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Safad.Models.Metric", "Metric")
                        .WithMany("ConfigurationMetric")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Safad.Models.Phase", "Phase")
                        .WithMany("ConfigurationMetric")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Metric");

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("Safad.Models.Division", b =>
                {
                    b.HasOne("Safad.Models.Category", "Category")
                        .WithMany("Division")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Safad.Models.GoalIndicator", b =>
                {
                    b.HasOne("Safad.Models.Metric", "Metric")
                        .WithMany("GoalIndicator")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Safad.Models.User_Athlete", "User_Athlete")
                        .WithMany("GoalIndicator")
                        .HasForeignKey("UserAthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Metric");

                    b.Navigation("User_Athlete");
                });

            modelBuilder.Entity("Safad.Models.Profesional", b =>
                {
                    b.HasOne("Safad.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Safad.Models.Team", b =>
                {
                    b.HasOne("Safad.Models.Category", "Category")
                        .WithMany("Teams")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Safad.Models.Division", "Division")
                        .WithMany("Teams")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Safad.Models.UserCoach", "UserCoach")
                        .WithOne("Team")
                        .HasForeignKey("Safad.Models.Team", "UserCoachId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Division");

                    b.Navigation("UserCoach");
                });

            modelBuilder.Entity("Safad.Models.TeamProfessional", b =>
                {
                    b.HasOne("Safad.Models.Profesional", "Profesional")
                        .WithOne("TeamProfessional")
                        .HasForeignKey("Safad.Models.TeamProfessional", "ProfesionalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Safad.Models.Team", "Team")
                        .WithOne("TeamProfessional")
                        .HasForeignKey("Safad.Models.TeamProfessional", "TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Profesional");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Safad.Models.TeamUserAthlete", b =>
                {
                    b.HasOne("Safad.Models.Team", "Team")
                        .WithMany("TeamUserAthletes")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Safad.Models.User_Athlete", "User_Athlete")
                        .WithMany("TeamUserAthletes")
                        .HasForeignKey("UserAthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User_Athlete");
                });

            modelBuilder.Entity("Safad.Models.User", b =>
                {
                    b.HasOne("Safad.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Safad.Models.UserCoach", b =>
                {
                    b.HasOne("Safad.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Safad.Models.User_Athlete", b =>
                {
                    b.HasOne("Safad.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Safad.Models.Category", b =>
                {
                    b.Navigation("ConfigurationMetric");

                    b.Navigation("Division");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Safad.Models.Division", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Safad.Models.Metric", b =>
                {
                    b.Navigation("ConfigurationMetric");

                    b.Navigation("GoalIndicator");
                });

            modelBuilder.Entity("Safad.Models.Phase", b =>
                {
                    b.Navigation("ConfigurationMetric");
                });

            modelBuilder.Entity("Safad.Models.Profesional", b =>
                {
                    b.Navigation("TeamProfessional")
                        .IsRequired();
                });

            modelBuilder.Entity("Safad.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Safad.Models.Team", b =>
                {
                    b.Navigation("TeamProfessional")
                        .IsRequired();

                    b.Navigation("TeamUserAthletes");
                });

            modelBuilder.Entity("Safad.Models.UserCoach", b =>
                {
                    b.Navigation("Team")
                        .IsRequired();
                });

            modelBuilder.Entity("Safad.Models.User_Athlete", b =>
                {
                    b.Navigation("GoalIndicator");

                    b.Navigation("TeamUserAthletes");
                });
#pragma warning restore 612, 618
        }
    }
}
