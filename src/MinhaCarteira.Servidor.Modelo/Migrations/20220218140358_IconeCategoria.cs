using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class IconeCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icone",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icone",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "Categoria");
        }
    }
}
