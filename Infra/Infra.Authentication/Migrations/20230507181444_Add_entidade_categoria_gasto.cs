﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class Addentidadecategoriagasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "Autenticacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gasto",
                schema: "Autenticacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Data = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gasto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "Autenticacao",
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_CategoriaId",
                schema: "Autenticacao",
                table: "Gasto",
                column: "CategoriaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gasto",
                schema: "Autenticacao");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "Autenticacao");
        }
    }
}
