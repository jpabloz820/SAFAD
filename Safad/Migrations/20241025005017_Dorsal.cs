using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class Dorsal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FootballNumber",
                table: "TeamUserAthletes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FootballNumber",
                table: "TeamUserAthletes");
        }
    }
}
