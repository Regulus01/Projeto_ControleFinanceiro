using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    /// <inheritdoc />
    public partial class altertelefonelong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Telefone",
                schema: "Gerencia",
                table: "Pessoa",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                schema: "Gerencia",
                table: "Pessoa",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
