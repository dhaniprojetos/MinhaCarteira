using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class CentroEMovimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentroClassificacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EhDespesa = table.Column<bool>(type: "bit", nullable: false),
                    EhReceita = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroClassificacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoBancario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimento = table.Column<int>(type: "int", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    CentroClassificacaoId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    ContaBancariaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoBancario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                        column: x => x.CentroClassificacaoId,
                        principalTable: "CentroClassificacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_CategoriaId",
                table: "MovimentoBancario",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_CentroClassificacaoId",
                table: "MovimentoBancario",
                column: "CentroClassificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_ContaBancariaId",
                table: "MovimentoBancario",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_PessoaId",
                table: "MovimentoBancario",
                column: "PessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentoBancario");

            migrationBuilder.DropTable(
                name: "CentroClassificacao");
        }
    }
}
