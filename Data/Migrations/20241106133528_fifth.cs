using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemType",
                table: "Permissions");

            migrationBuilder.AddColumn<Guid>(
                name: "ActionId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SystemId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ViewId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SystemId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SystemId",
                table: "ApplicationUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ActionId",
                table: "Permissions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ViewId",
                table: "Permissions",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SystemId",
                table: "Groups",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_SystemId",
                table: "ApplicationUser",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_SystemId",
                table: "Actions",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_SystemId",
                table: "Views",
                column: "SystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Systems_SystemId",
                table: "ApplicationUser",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Systems_SystemId",
                table: "Groups",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Actions_ActionId",
                table: "Permissions",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Systems_SystemId",
                table: "Permissions",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Views_ViewId",
                table: "Permissions",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Systems_SystemId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Systems_SystemId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Actions_ActionId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Systems_SystemId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Views_ViewId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ActionId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_SystemId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ViewId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SystemId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_SystemId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ViewId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemType",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Groups_GroupId",
                table: "GroupPermission",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
