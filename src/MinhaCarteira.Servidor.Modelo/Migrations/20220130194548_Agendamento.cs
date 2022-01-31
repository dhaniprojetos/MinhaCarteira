using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class Agendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria");

            migrationBuilder.RenameColumn(
                name: "IdInstituicaoFinanceira",
                table: "ContaBancaria",
                newName: "InstituicaoFinanceiraId");

            migrationBuilder.RenameIndex(
                name: "IX_ContaBancaria_IdInstituicaoFinanceira",
                table: "ContaBancaria",
                newName: "IX_ContaBancaria_InstituicaoFinanceiraId");

            migrationBuilder.AddColumn<int>(
                name: "AgendamentoItemId",
                table: "MovimentoBancario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    RegraRecorrencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CentroClassificacaoId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true),
                    ContaBancariaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamento_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agendamento_CentroClassificacao_CentroClassificacaoId",
                        column: x => x.CentroClassificacaoId,
                        principalTable: "CentroClassificacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agendamento_ContaBancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agendamento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgendamentoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgendamentoId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    EstahPaga = table.Column<bool>(type: "bit", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true),
                    ContaBancariaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendamentoItem_Agendamento_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentoItem_ContaBancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgendamentoItem_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_AgendamentoItemId",
                table: "MovimentoBancario",
                column: "AgendamentoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_CategoriaId",
                table: "Agendamento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_CentroClassificacaoId",
                table: "Agendamento",
                column: "CentroClassificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ContaBancariaId",
                table: "Agendamento",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_PessoaId",
                table: "Agendamento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoItem_AgendamentoId",
                table: "AgendamentoItem",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoItem_ContaBancariaId",
                table: "AgendamentoItem",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoItem_PessoaId",
                table: "AgendamentoItem",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_InstituicaoFinanceiraId",
                table: "ContaBancaria",
                column: "InstituicaoFinanceiraId",
                principalTable: "InstituicaoFinanceira",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_AgendamentoItem_AgendamentoItemId",
                table: "MovimentoBancario",
                column: "AgendamentoItemId",
                principalTable: "AgendamentoItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_InstituicaoFinanceiraId",
                table: "ContaBancaria");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_AgendamentoItem_AgendamentoItemId",
                table: "MovimentoBancario");

            migrationBuilder.DropTable(
                name: "AgendamentoItem");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_MovimentoBancario_AgendamentoItemId",
                table: "MovimentoBancario");

            migrationBuilder.DropColumn(
                name: "AgendamentoItemId",
                table: "MovimentoBancario");

            migrationBuilder.RenameColumn(
                name: "InstituicaoFinanceiraId",
                table: "ContaBancaria",
                newName: "IdInstituicaoFinanceira");

            migrationBuilder.RenameIndex(
                name: "IX_ContaBancaria_InstituicaoFinanceiraId",
                table: "ContaBancaria",
                newName: "IX_ContaBancaria_IdInstituicaoFinanceira");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria",
                column: "IdInstituicaoFinanceira",
                principalTable: "InstituicaoFinanceira",
                principalColumn: "Id");
        }
    }
}
