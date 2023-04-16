using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    /// <inheritdoc />
    public partial class AddEnderecoPessoaPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                schema: "Gerencia",
                table: "Pessoa");

            migrationBuilder.AddColumn<Guid>(
                name: "id_endereco",
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_id_endereco",
                schema: "Gerencia",
                table: "Pessoa",
                column: "id_endereco",
                unique: true);
            
            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Endereco_id_endereco",
                schema: "Gerencia",
                table: "Pessoa",
                column: "id_endereco",
                principalSchema: "Gerencia",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Endereco_id_endereco",
                schema: "Gerencia",
                table: "Pessoa");

            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "Gerencia");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_id_endereco",
                schema: "Gerencia",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "id_endereco",
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
