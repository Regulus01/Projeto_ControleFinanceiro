using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Gerencia.Migrations
{
    /// <inheritdoc />
    public partial class InsertColumnSaldoPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saldo",
                schema: "Gerencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataInsercao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saldo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saldo_Pessoa_Id",
                        column: x => x.Id,
                        principalSchema: "Gerencia",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Saldo_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "Gerencia",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saldo_PessoaId",
                schema: "Gerencia",
                table: "Saldo",
                column: "PessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Saldo",
                schema: "Gerencia");
        }
    }
}
