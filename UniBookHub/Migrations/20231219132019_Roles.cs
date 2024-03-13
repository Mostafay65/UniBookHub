using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniBookHub.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2decac30-3045-480c-8da7-a34135e68399", null, "Admin", "ADMIN" },
                    { "b7f7d1f2-b1fa-4ab8-96e0-43cb727313d7", null, "IT", "IT" },
                    { "ee1dbdab-5a15-4f05-8993-9471461f13e6", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CollegeId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1fd46020-adeb-4454-ba62-6bbb1e34db0e", 0, null, "fdaf5a19-f740-4300-8a4f-fa0e73fedbbf", null, false, false, null, "Super Admin", null, "ADMIN", "Admin", null, null, false, "bb6d3d72-1962-4b8d-a571-170ac132790a", false, "Admin" });
        }
        //  7a711826-9665-4412-a106-69e004e25f86,Student,STUDENT,
        //  f59911c4-4df0-4f36-a8aa-6ca5916d605e,Admin,ADMIN,


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2decac30-3045-480c-8da7-a34135e68399");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f7d1f2-b1fa-4ab8-96e0-43cb727313d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee1dbdab-5a15-4f05-8993-9471461f13e6");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1fd46020-adeb-4454-ba62-6bbb1e34db0e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d12a4ec3-0055-4fea-9cfe-e6a76b17f5f9", null, "Admin", null },
                    { "ebf0c5a9-df4f-430d-b772-08e8a5bad95a", null, "Student", null },
                    { "f3bd74fd-303f-4f07-96e5-7d71122cdbef", null, "IT", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CollegeId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d8b7629-e103-4196-aedb-0b851c9dec54", 0, null, "7736dbc7-f37c-4867-b65f-f13d64221cbb", null, false, false, null, "Super Admin", null, null, "Admin", null, null, false, "8f72b759-0d4f-4c45-9545-184c74b0469e", false, "Admin" });
        }
    }
}
