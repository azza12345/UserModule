using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class six : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Systems_SystemId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SystemId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions",
                column: "SystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Systems_SystemId",
                table: "Permissions",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id");
        }
    }
}
