using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class GoalIndicator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoalIndicators",
                columns: table => new
                {
                    GoalIndicatorId = table.Column<int>(type: "int", nullable: false),
                    UserAthleteId = table.Column<int>(type: "int", nullable: false),
                    MetricId = table.Column<int>(type: "int", nullable: false),
                    MeasureAthlete = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalIndicators", x => x.GoalIndicatorId);
                    table.ForeignKey(
                        name: "FK_GoalIndicators_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "MetricId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoalIndicators_UserAthletes_UserAthleteId",
                        column: x => x.UserAthleteId,
                        principalTable: "UserAthletes",
                        principalColumn: "UserAthleteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalIndicators_MetricId",
                table: "GoalIndicators",
                column: "MetricId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalIndicators_UserAthleteId",
                table: "GoalIndicators",
                column: "UserAthleteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalIndicators");
        }
    }
}
