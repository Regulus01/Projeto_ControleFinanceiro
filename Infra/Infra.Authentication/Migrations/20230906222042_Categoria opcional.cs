using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class Categoriaopcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "CategoriaId",
                principalSchema: "Autenticacao",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "CategoriaId",
                principalSchema: "Autenticacao",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
