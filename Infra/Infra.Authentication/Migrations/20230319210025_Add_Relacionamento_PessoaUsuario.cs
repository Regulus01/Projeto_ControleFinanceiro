using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionamentoPessoaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Autenticacao");

            migrationBuilder.RenameTable(
                name: "Usuario",
                schema: "Pessoa",
                newName: "Usuario",
                newSchema: "Autenticacao");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "Pessoa",
                newName: "Role",
                newSchema: "Autenticacao");

            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                schema: "Autenticacao",
                table: "Usuario",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Usuario_Id",
                table: "Pessoa",
                schema: "Gerencia",
                column: "Id",
                principalSchema: "Autenticacao",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.DropSchema(
                name: "Pessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Usuario_Id",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                schema: "Autenticacao",
                table: "Usuario");

            migrationBuilder.EnsureSchema(
                name: "Pessoa");

            migrationBuilder.RenameTable(
                name: "Usuario",
                schema: "Autenticacao",
                newName: "Usuario",
                newSchema: "Pessoa");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "Autenticacao",
                newName: "Role",
                newSchema: "Pessoa");
            
            migrationBuilder.DropSchema(
                name: "Autenticacao");
        }
    }
}
