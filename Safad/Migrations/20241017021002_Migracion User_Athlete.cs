using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class MigracionUser_Athlete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAthletes",
                columns: table => new
                {
                    UserAthleteId = table.Column<int>(type: "int", nullable: false),
                    NameAthlete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DniAthlete = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAthletes", x => x.UserAthleteId);
                    table.ForeignKey(
                        name: "FK_UserAthletes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAthletes_UserId",
                table: "UserAthletes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAthletes");
        }
    }
}
