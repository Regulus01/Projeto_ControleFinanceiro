using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class altercategoriaidgasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gasto_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gasto_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "CategoriaId",
                unique: true);
        }
    }
}
