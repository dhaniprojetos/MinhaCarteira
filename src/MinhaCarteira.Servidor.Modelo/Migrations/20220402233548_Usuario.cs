using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaCarteira.Servidor.Modelo.Migrations
{
    public partial class Usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PapelIds",
                startValue: 100L);

            migrationBuilder.CreateSequence<int>(
                name: "UsuarioIds",
                startValue: 100L);

            migrationBuilder.CreateTable(
                name: "Papel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR PapelIds"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UsuarioIds"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPapel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PapelId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPapel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPapel_Papel_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPapel_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Papel",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Papel",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "Nome", "PasswordHash", "Sobrenome", "Username" },
                values: new object[] { 1, "administrador@dhaniprojetos.com", "Administrador", "AB4+r9Q4eYueiDzAeZloJYLY4OtcX5Zp52zjvkOtrZn7bX210wvRb1/+QJFIe9FzQg==", "do Sistema", "admin" });

            migrationBuilder.InsertData(
                table: "UsuarioPapel",
                columns: new[] { "Id", "PapelId", "UsuarioId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UsuarioPapel",
                columns: new[] { "Id", "PapelId", "UsuarioId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPapel_PapelId",
                table: "UsuarioPapel",
                column: "PapelId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPapel_UsuarioId",
                table: "UsuarioPapel",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPapel");

            migrationBuilder.DropTable(
                name: "Papel");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropSequence(
                name: "PapelIds");

            migrationBuilder.DropSequence(
                name: "UsuarioIds");
        }
    }
}
