using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniBookHub.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23941cb5-c546-4b18-b96f-b956896572f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f34cf49-b44a-4287-ba3c-94138685a301");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a722b0a9-91a0-4fc9-8ca6-055d37f74dc4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "318e5c84-5ada-49b0-9dcf-dec9fcaea02a", null, "Student", "STUDENT" },
                    { "f0359250-ca2d-40bc-ae90-c7f7569ca72d", null, "Admin", "ADMIN" },
                    { "f558d1ab-3d1d-40c4-b610-d6f047649e39", null, "IT Administrator", "IT ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "318e5c84-5ada-49b0-9dcf-dec9fcaea02a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0359250-ca2d-40bc-ae90-c7f7569ca72d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f558d1ab-3d1d-40c4-b610-d6f047649e39");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23941cb5-c546-4b18-b96f-b956896572f6", null, "IT Administrator", "IT ADMINISTRATOR" },
                    { "3f34cf49-b44a-4287-ba3c-94138685a301", null, "Student", "STUDENT" },
                    { "a722b0a9-91a0-4fc9-8ca6-055d37f74dc4", null, "Admin", "ADMIN" }
                });
        }
    }
}
