using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class AgendamentoDataInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicial",
                table: "Agendamento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInicial",
                table: "Agendamento");
        }
    }
}
