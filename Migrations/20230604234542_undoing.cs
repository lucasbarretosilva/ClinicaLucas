using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaLucas.Migrations
{
    public partial class undoing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exame_Consulta_ConsultaId",
                table: "Exame");

            migrationBuilder.DropIndex(
                name: "IX_Exame_ConsultaId",
                table: "Exame");

            migrationBuilder.DropColumn(
                name: "ConsultaId",
                table: "Exame");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsultaId",
                table: "Exame",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exame_ConsultaId",
                table: "Exame",
                column: "ConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exame_Consulta_ConsultaId",
                table: "Exame",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id");
        }
    }
}
