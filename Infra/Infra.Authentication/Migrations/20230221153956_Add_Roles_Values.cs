using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Pessoa",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    "8f17a556-fa8b-4abe-87d2-c164e41eef39",
                    "admin"
                });
            
            migrationBuilder.InsertData(
                schema: "Pessoa",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[]
                {
                    "fc1eb138-1c84-4fb0-846b-0d8f45d6aac3",
                    "cliente"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Pessoa",
                table: "Role",
                keyColumn: "Id",
                keyValue: "fc1eb138-1c84-4fb0-846b-0d8f45d6aac3"
            );
        }
    }
}
