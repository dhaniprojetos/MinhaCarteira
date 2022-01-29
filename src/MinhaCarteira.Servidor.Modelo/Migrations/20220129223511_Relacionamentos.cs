using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class Relacionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Categoria_IdCategoriaPai",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_Categoria_CategoriaId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_Pessoa_PessoaId",
                table: "MovimentoBancario");

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "InstituicaoFinanceira",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Categoria_IdCategoriaPai",
                table: "Categoria",
                column: "IdCategoriaPai",
                principalTable: "Categoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria",
                column: "IdInstituicaoFinanceira",
                principalTable: "InstituicaoFinanceira",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_Categoria_CategoriaId",
                table: "MovimentoBancario",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                table: "MovimentoBancario",
                column: "CentroClassificacaoId",
                principalTable: "CentroClassificacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                table: "MovimentoBancario",
                column: "ContaBancariaId",
                principalTable: "ContaBancaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_Pessoa_PessoaId",
                table: "MovimentoBancario",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Categoria_IdCategoriaPai",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_Categoria_CategoriaId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                table: "MovimentoBancario");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoBancario_Pessoa_PessoaId",
                table: "MovimentoBancario");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "InstituicaoFinanceira");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Categoria_IdCategoriaPai",
                table: "Categoria",
                column: "IdCategoriaPai",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancaria_InstituicaoFinanceira_IdInstituicaoFinanceira",
                table: "ContaBancaria",
                column: "IdInstituicaoFinanceira",
                principalTable: "InstituicaoFinanceira",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_Categoria_CategoriaId",
                table: "MovimentoBancario",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_CentroClassificacao_CentroClassificacaoId",
                table: "MovimentoBancario",
                column: "CentroClassificacaoId",
                principalTable: "CentroClassificacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_ContaBancaria_ContaBancariaId",
                table: "MovimentoBancario",
                column: "ContaBancariaId",
                principalTable: "ContaBancaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoBancario_Pessoa_PessoaId",
                table: "MovimentoBancario",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
