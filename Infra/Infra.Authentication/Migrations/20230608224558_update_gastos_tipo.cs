using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class updategastostipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                schema: "Autenticacao",
                table: "Gasto",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                schema: "Autenticacao",
                table: "Gasto");
        }
    }
}
