using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdCategoriaPai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Categoria_IdCategoriaPai",
                        column: x => x.IdCategoriaPai,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                });

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
                name: "InstituicaoFinanceira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituicaoFinanceira", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EhCliente = table.Column<bool>(type: "bit", nullable: false),
                    EhFornecedor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Conta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InstituicaoFinanceiraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaBancaria_InstituicaoFinanceira_InstituicaoFinanceiraId",
                        column: x => x.InstituicaoFinanceiraId,
                        principalTable: "InstituicaoFinanceira",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    RegraRecorrencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parcelas = table.Column<int>(type: "int", nullable: false),
                    TipoParcelas = table.Column<int>(type: "int", nullable: false),
                    TipoRecorrencia = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "MovimentoBancario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxiliar = table.Column<int>(type: "int", nullable: true),
                    TipoMovimento = table.Column<int>(type: "int", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    CentroClassificacaoId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    ContaBancariaId = table.Column<int>(type: "int", nullable: false),
                    AgendamentoItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoBancario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_AgendamentoItem_AgendamentoItemId",
                        column: x => x.AgendamentoItemId,
                        principalTable: "AgendamentoItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                        column: x => x.CentroClassificacaoId,
                        principalTable: "CentroClassificacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimentoBancario_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_IdCategoriaPai",
                table: "Categoria",
                column: "IdCategoriaPai");

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancaria_InstituicaoFinanceiraId",
                table: "ContaBancaria",
                column: "InstituicaoFinanceiraId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBancario_AgendamentoItemId",
                table: "MovimentoBancario",
                column: "AgendamentoItemId");

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
                name: "AgendamentoItem");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "CentroClassificacao");

            migrationBuilder.DropTable(
                name: "ContaBancaria");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "InstituicaoFinanceira");
        }
    }
}
