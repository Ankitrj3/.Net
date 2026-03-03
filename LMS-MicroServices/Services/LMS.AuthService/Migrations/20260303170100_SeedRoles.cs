using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.AuthService.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "11111111-1111-1111-1111-111111111111", "Admin", "ADMIN" },
                    { "22222222-2222-2222-2222-222222222222", "22222222-2222-2222-2222-222222222222", "User", "USER" },
                    { "33333333-3333-3333-3333-333333333333", "33333333-3333-3333-3333-333333333333", "Instructor", "INSTRUCTOR" },
                    { "44444444-4444-4444-4444-444444444444", "44444444-4444-4444-4444-444444444444", "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44444444-4444-4444-4444-444444444444");
        }
    }
}
