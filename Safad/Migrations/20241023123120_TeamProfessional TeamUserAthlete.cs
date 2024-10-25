using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class TeamProfessionalTeamUserAthlete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAthletes_TeamId",
                table: "UserAthletes");

            migrationBuilder.DropIndex(
                name: "IX_Profesional_TeamId",
                table: "Profesional");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "UserAthletes");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Profesional");

            migrationBuilder.CreateTable(
                name: "TeamProfessionals",
                columns: table => new
                {
                    TeamProfessionalId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ProfesionalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamProfessionals", x => x.TeamProfessionalId);
                    table.ForeignKey(
                        name: "FK_TeamProfessionals_Profesional_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesional",
                        principalColumn: "ProfesionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamProfessionals_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamUserAthletes",
                columns: table => new
                {
                    TeamUserAthleteId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    UserAthleteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUserAthletes", x => x.TeamUserAthleteId);
                    table.ForeignKey(
                        name: "FK_TeamUserAthletes_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamUserAthletes_UserAthletes_UserAthleteId",
                        column: x => x.UserAthleteId,
                        principalTable: "UserAthletes",
                        principalColumn: "UserAthleteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamProfessionals_ProfesionalId",
                table: "TeamProfessionals",
                column: "ProfesionalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamProfessionals_TeamId",
                table: "TeamProfessionals",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamUserAthletes_TeamId",
                table: "TeamUserAthletes",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamUserAthletes_UserAthleteId",
                table: "TeamUserAthletes",
                column: "UserAthleteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamProfessionals");

            migrationBuilder.DropTable(
                name: "TeamUserAthletes");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "UserAthletes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Profesional",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAthletes_TeamId",
                table: "UserAthletes",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Profesional_TeamId",
                table: "Profesional",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesional_Teams_TeamId",
                table: "Profesional",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAthletes_Teams_TeamId",
                table: "UserAthletes",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
