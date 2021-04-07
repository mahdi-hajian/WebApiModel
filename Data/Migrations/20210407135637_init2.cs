using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 1, "566496df-d9e0-452e-9582-da7d18f167b3", "ادمین اصلی سایت", "Leader", "LEADER" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 2, "46300f66-9941-4ca3-b695-11f6fc71e601", "جانشین", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "Gender", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, 24, "05d511da-7eb0-43a1-a3c6-8ead19d7aa3d", "admin@mahdihajian.ir", true, "mahdi hajian", 1, true, null, false, null, "ADMIN@MAHDIHAJIAN.IR", "MAHDI_HAJIAN", "AQAAAAEAACcQAAAAEJk1i5ZEtS18TxyqdWWC1Vk6Eczra7x0KSf0nUEBaOtvRh3T7LN/YG0znWwSTJRCsQ==", null, true, "01b1d3f3-1740-4635-abf9-9eceefbb1ebf", false, "mahdi_hajian" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
