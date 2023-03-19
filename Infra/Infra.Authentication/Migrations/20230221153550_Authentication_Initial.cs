using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticationInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Pessoa");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 160, nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "VARCHAR", maxLength: 80, nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Pessoa",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Slug",
                schema: "Pessoa",
                table: "Usuario",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RoleId",
                schema: "Pessoa",
                table: "Usuario",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Pessoa");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Pessoa");

            migrationBuilder.DropSchema(
                name: "Pessoa");
        }
    }
}
