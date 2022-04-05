using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class PreferenciasUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioPreferencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPreferencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPreferencia_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AHznCCgmxg11MlXakLmaFTxPYEmMEQoChLBkBjcFOvTTYbiVjsxHqjGdqAc36sgASg==");

            migrationBuilder.InsertData(
                table: "UsuarioPreferencia",
                columns: new[] { "Id", "Nome", "UsuarioId", "Valor" },
                values: new object[] { 1, "remember.lte.pushmenu", 1, "sidebar-collapsed" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPreferencia_UsuarioId",
                table: "UsuarioPreferencia",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPreferencia");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AB4+r9Q4eYueiDzAeZloJYLY4OtcX5Zp52zjvkOtrZn7bX210wvRb1/+QJFIe9FzQg==");
        }
    }
}
