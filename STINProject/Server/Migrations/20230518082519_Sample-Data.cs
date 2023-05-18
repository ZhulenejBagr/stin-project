using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STINProject.Server.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class SampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("4a19af75-4b2e-481b-af51-2a8795f8b637"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Username" },
                values: new object[] { new Guid("fc9937db-a031-48ad-9397-088d3c3cfdac"), "sampleuser@gmail.com", "password", "sampleuser" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance", "Currency", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("003bceb8-2bf6-4083-a691-6adefbef9088"), 0.0, "USD", new Guid("fc9937db-a031-48ad-9397-088d3c3cfdac") },
                    { new Guid("bf61e44f-dc39-4aa3-8f7e-a66b051b8a17"), 100.0, "CZK", new Guid("fc9937db-a031-48ad-9397-088d3c3cfdac") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("003bceb8-2bf6-4083-a691-6adefbef9088"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("bf61e44f-dc39-4aa3-8f7e-a66b051b8a17"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("fc9937db-a031-48ad-9397-088d3c3cfdac"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Username" },
                values: new object[] { new Guid("4a19af75-4b2e-481b-af51-2a8795f8b637"), "sus@bus.com", "bus", "sus" });
        }
    }
}
