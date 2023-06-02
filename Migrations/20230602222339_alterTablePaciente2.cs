using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaLucas.Migrations
{
    public partial class alterTablePaciente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SexoPaciente",
                table: "Paciente",
                newName: "Sexo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Paciente",
                newName: "SexoPaciente");
        }
    }
}
