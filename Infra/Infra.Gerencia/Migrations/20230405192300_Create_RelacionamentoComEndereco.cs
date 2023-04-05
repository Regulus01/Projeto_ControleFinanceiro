using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelacionamentoComEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                schema: "Gerencia",
                table: "Pessoa");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                schema: "Gerencia",
                table: "Pessoa",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "Gerencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cep = table.Column<int>(type: "integer", maxLength: 8, nullable: false),
                    Logradouro = table.Column<string>(type: "text", nullable: false),
                    Bairro = table.Column<string>(type: "text", nullable: false),
                    Localidade = table.Column<string>(type: "text", nullable: false),
                    Uf = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Pessoa_Id",
                        column: x => x.Id,
                        principalSchema: "Gerencia",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "Gerencia");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                schema: "Gerencia",
                table: "Pessoa");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                schema: "Gerencia",
                table: "Pessoa",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
