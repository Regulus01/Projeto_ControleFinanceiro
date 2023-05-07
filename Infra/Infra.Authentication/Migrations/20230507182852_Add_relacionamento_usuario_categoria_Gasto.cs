using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddrelacionamentousuariocategoriaGasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                schema: "Autenticacao",
                table: "Gasto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                schema: "Autenticacao",
                table: "Categoria",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_UsuarioId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UsuarioId",
                schema: "Autenticacao",
                table: "Categoria",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Usuario_UsuarioId",
                schema: "Autenticacao",
                table: "Categoria",
                column: "UsuarioId",
                principalSchema: "Autenticacao",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "UsuarioId",
                principalSchema: "Autenticacao",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Usuario_UsuarioId",
                schema: "Autenticacao",
                table: "Categoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_UsuarioId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_UsuarioId",
                schema: "Autenticacao",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Autenticacao",
                table: "Categoria");
        }
    }
}
