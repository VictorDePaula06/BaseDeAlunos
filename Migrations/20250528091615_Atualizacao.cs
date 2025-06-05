using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseAlunos.Migrations
{
    public partial class Atualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Alunos",
                newName: "Logradouro");

            migrationBuilder.AddColumn<int>(
                name: "NumeroLogradouro",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroLogradouro",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Alunos",
                newName: "Endereco");
        }
    }
}
