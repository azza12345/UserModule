using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ensurethateverythingCommited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { new Guid("5d6bd4f3-359a-4f8a-882c-8df11bd4d147"), new Guid("e6a2f00b-a013-4338-8e79-54871b3f75d2") });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c7d0fb11-69eb-4f0b-a55f-7cb41a810b01"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("5d6bd4f3-359a-4f8a-882c-8df11bd4d147"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e6a2f00b-a013-4338-8e79-54871b3f75d2"));

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: new Guid("206218bd-4133-48e0-884a-733f599e80aa"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("9c93be5b-0b03-4c27-a40b-e6fae965fc83"));

            migrationBuilder.DeleteData(
                table: "Systems",
                keyColumn: "Id",
                keyValue: new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a"));

            migrationBuilder.InsertData(
                table: "Systems",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993"), " default system ", "Default System" });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "Description", "Name", "SystemId" },
                values: new object[] { new Guid("292fce7b-2977-4e6d-9e49-51b32b6a6fcc"), "Allow User creating new users", "Create User", new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993") });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDate", "Description", "IsActive", "Name", "ParentGroupId", "SystemId", "UpdatedByUserId", "UpdatedDate" },
                values: new object[] { new Guid("b0bbb8ad-7ecf-478f-866c-5edbd9b3426e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meters System ", true, "Meters", null, new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993"), null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedByUserId", "CreatedDate", "Email", "EmailConfirmed", "Fname", "IsActive", "IsEmailVerified", "IsMobileVerified", "Lname", "LockoutEnabled", "LockoutEnd", "Mobile", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SystemId", "TwoFactorEnabled", "UpdatedByUserId", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("aa8befc9-8ea7-45b3-8158-bd912b078a49"), 0, "6Th October", "0f21e835-de9b-4e22-9f99-a744eb41ea8f", null, new DateTime(2024, 12, 8, 14, 52, 25, 143, DateTimeKind.Local).AddTicks(8258), "azzaAdmin@gmail.com", false, "Azza", false, false, false, "Mohamed", false, null, "1234567890", null, null, "Azza123#", null, false, null, new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993"), false, null, null, null });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "Description", "Name", "SystemId" },
                values: new object[] { new Guid("6c0582d1-41f3-4363-a950-d737330b560d"), "admin dashboard", "Admin Dashboard", new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993") });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ActionId", "ViewId" },
                values: new object[] { new Guid("14a083d3-c8e4-42ee-adea-3da00784e5e8"), new Guid("292fce7b-2977-4e6d-9e49-51b32b6a6fcc"), new Guid("6c0582d1-41f3-4363-a950-d737330b560d") });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[] { new Guid("b0bbb8ad-7ecf-478f-866c-5edbd9b3426e"), new Guid("14a083d3-c8e4-42ee-adea-3da00784e5e8") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { new Guid("b0bbb8ad-7ecf-478f-866c-5edbd9b3426e"), new Guid("14a083d3-c8e4-42ee-adea-3da00784e5e8") });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("aa8befc9-8ea7-45b3-8158-bd912b078a49"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("b0bbb8ad-7ecf-478f-866c-5edbd9b3426e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("14a083d3-c8e4-42ee-adea-3da00784e5e8"));

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: new Guid("292fce7b-2977-4e6d-9e49-51b32b6a6fcc"));

            migrationBuilder.DeleteData(
                table: "Views",
                keyColumn: "Id",
                keyValue: new Guid("6c0582d1-41f3-4363-a950-d737330b560d"));

            migrationBuilder.DeleteData(
                table: "Systems",
                keyColumn: "Id",
                keyValue: new Guid("3ca46190-b2a3-403d-8b36-bc0d2035a993"));

            migrationBuilder.InsertData(
                table: "Systems",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a"), " default system ", "Default System" });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "Description", "Name", "SystemId" },
                values: new object[] { new Guid("206218bd-4133-48e0-884a-733f599e80aa"), "Allow User creating new users", "Create User", new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a") });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDate", "Description", "IsActive", "Name", "ParentGroupId", "SystemId", "UpdatedByUserId", "UpdatedDate" },
                values: new object[] { new Guid("5d6bd4f3-359a-4f8a-882c-8df11bd4d147"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Administrators ", true, "Administrators", null, new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a"), null, null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedByUserId", "CreatedDate", "Email", "EmailConfirmed", "Fname", "IsActive", "IsEmailVerified", "IsMobileVerified", "Lname", "LockoutEnabled", "LockoutEnd", "Mobile", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SystemId", "TwoFactorEnabled", "UpdatedByUserId", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("c7d0fb11-69eb-4f0b-a55f-7cb41a810b01"), 0, "6Th October", "15b2c748-1f88-4081-9892-468d6b86490e", null, new DateTime(2024, 11, 21, 14, 54, 22, 1, DateTimeKind.Local).AddTicks(3531), "azzaAdmin@gmail.com", false, "Azza", false, false, false, "Mohamed", false, null, "1234567890", null, null, "Azza123#", null, false, null, new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a"), false, null, null, null });

            migrationBuilder.InsertData(
                table: "Views",
                columns: new[] { "Id", "Description", "Name", "SystemId" },
                values: new object[] { new Guid("9c93be5b-0b03-4c27-a40b-e6fae965fc83"), "admin dashboard", "Admin Dashboard", new Guid("104ae8c0-3c90-4268-8453-d68a23b9e67a") });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ActionId", "ViewId" },
                values: new object[] { new Guid("e6a2f00b-a013-4338-8e79-54871b3f75d2"), new Guid("206218bd-4133-48e0-884a-733f599e80aa"), new Guid("9c93be5b-0b03-4c27-a40b-e6fae965fc83") });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[] { new Guid("5d6bd4f3-359a-4f8a-882c-8df11bd4d147"), new Guid("e6a2f00b-a013-4338-8e79-54871b3f75d2") });
        }
    }
}
