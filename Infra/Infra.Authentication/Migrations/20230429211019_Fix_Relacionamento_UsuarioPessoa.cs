using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class FixRelacionamentoUsuarioPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Usuario_Id",
                schema: "Autenticacao",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PessoaId",
                schema: "Autenticacao",
                table: "Usuario",
                column: "PessoaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Pessoa_PessoaId",
                schema: "Autenticacao",
                table: "Usuario",
                column: "PessoaId",
                principalSchema: "Gerencia",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Pessoa_PessoaId",
                schema: "Autenticacao",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PessoaId",
                schema: "Autenticacao",
                table: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Usuario_Id",
                schema: "Gerencia",
                table: "Pessoa",
                column: "Id",
                principalSchema: "Autenticacao",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
