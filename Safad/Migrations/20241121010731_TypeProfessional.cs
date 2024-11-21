using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Safad.Migrations
{
    /// <inheritdoc />
    public partial class TypeProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeProfessionalId",
                table: "Profesional",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeProfessional",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    NameType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProfessional", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profesional_TypeProfessionalId",
                table: "Profesional",
                column: "TypeProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesional_TypeProfessional_TypeProfessionalId",
                table: "Profesional",
                column: "TypeProfessionalId",
                principalTable: "TypeProfessional",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesional_TypeProfessional_TypeProfessionalId",
                table: "Profesional");

            migrationBuilder.DropTable(
                name: "TypeProfessional");

            migrationBuilder.DropIndex(
                name: "IX_Profesional_TypeProfessionalId",
                table: "Profesional");

            migrationBuilder.DropColumn(
                name: "TypeProfessionalId",
                table: "Profesional");
        }
    }
}
