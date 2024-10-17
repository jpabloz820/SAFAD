using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class CreaciondelaClaseTablaProfesional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profesional",
                columns: table => new
                {
                    ProfesionalId = table.Column<int>(type: "int", nullable: false),
                    NameProfesional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DniProfesional = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesional", x => x.ProfesionalId);
                    table.ForeignKey(
                        name: "FK_Profesional_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesional_UserId",
                table: "Profesional",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profesional");
        }
    }
}
