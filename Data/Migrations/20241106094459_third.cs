using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "CreatedByRecords",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedByRecords_CreatedByUserId",
                table: "CreatedByRecords",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedByRecords_UpdatedByUserId",
                table: "CreatedByRecords",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedByRecords_AspNetUsers_CreatedByUserId",
                table: "CreatedByRecords",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedByRecords_AspNetUsers_UpdatedByUserId",
                table: "CreatedByRecords",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedByRecords_AspNetUsers_CreatedByUserId",
                table: "CreatedByRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_CreatedByRecords_AspNetUsers_UpdatedByUserId",
                table: "CreatedByRecords");

            migrationBuilder.DropIndex(
                name: "IX_CreatedByRecords_CreatedByUserId",
                table: "CreatedByRecords");

            migrationBuilder.DropIndex(
                name: "IX_CreatedByRecords_UpdatedByUserId",
                table: "CreatedByRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "CreatedByRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
