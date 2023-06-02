using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaLucas.Migrations
{
    public partial class alterTablePaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SexoPaciente",
                table: "Paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SexoPaciente",
                table: "Paciente");
        }
    }
}
