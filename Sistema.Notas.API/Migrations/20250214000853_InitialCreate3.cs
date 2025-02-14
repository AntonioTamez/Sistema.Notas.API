using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema.Notas.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Calificaciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Calificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
