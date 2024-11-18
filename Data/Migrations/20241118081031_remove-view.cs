using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class removeview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Views_ViewId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Systems_SystemId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ViewId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Views",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "ViewId",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Views",
                newName: "View");

            migrationBuilder.RenameIndex(
                name: "IX_Views_SystemId",
                table: "View",
                newName: "IX_View_SystemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_View",
                table: "View",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_View_Systems_SystemId",
                table: "View",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_Systems_SystemId",
                table: "View");

            migrationBuilder.DropPrimaryKey(
                name: "PK_View",
                table: "View");

            migrationBuilder.RenameTable(
                name: "View",
                newName: "Views");

            migrationBuilder.RenameIndex(
                name: "IX_View_SystemId",
                table: "Views",
                newName: "IX_Views_SystemId");

            migrationBuilder.AddColumn<Guid>(
                name: "ViewId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Views",
                table: "Views",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ViewId",
                table: "Permissions",
                column: "ViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Views_ViewId",
                table: "Permissions",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Systems_SystemId",
                table: "Views",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id");
        }
    }
}
