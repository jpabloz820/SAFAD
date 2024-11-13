using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    MetricId = table.Column<int>(type: "int", nullable: false),
                    MetricName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indicator = table.Column<float>(type: "real", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.MetricId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.PhaseId);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationMetrics",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false),
                    MetricId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationMetrics", x => new { x.PhaseId, x.MetricId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ConfigurationMetrics_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationMetrics_Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "Metrics",
                        principalColumn: "MetricId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationMetrics_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "PhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Phases",
                columns: new[] { "PhaseId", "PhaseName" },
                values: new object[,]
                {
                    { 1, "Fase de Preparación Física" },
                    { 2, "Fase Técnica y Táctica" },
                    { 3, "Fase de Integración Táctica Colectiva" },
                    { 4, "Fase de Competencia" },
                    { 5, "Fase de Recuperación" },
                    { 6, "Fase de Transición" },
                    { 7, "Fase de Análisis de Rendimiento" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationMetrics_CategoryId",
                table: "ConfigurationMetrics",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationMetrics_MetricId",
                table: "ConfigurationMetrics",
                column: "MetricId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationMetrics");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "Phases");
        }
    }
}
