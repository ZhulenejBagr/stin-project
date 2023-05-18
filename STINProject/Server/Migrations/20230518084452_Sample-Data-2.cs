using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STINProject.Server.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class SampleData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4"), "sampleuser@gmail.com", "password", "sampleuser" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance", "Currency", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"), 100.0, "CZK", new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4") },
                    { new Guid("fe024929-3699-40b0-ab21-c619b1f83721"), 0.0, "USD", new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4") }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "AccountID", "Date", "Value" },
                values: new object[,]
                {
                    { new Guid("10030d26-c959-425e-87fe-f54b570d44eb"), new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"), new DateTime(2022, 1, 5, 0, 5, 0, 0, DateTimeKind.Unspecified), -30.0 },
                    { new Guid("18f9a75b-d766-4f4a-9347-6bafc0e5d4e6"), new Guid("fe024929-3699-40b0-ab21-c619b1f83721"), new DateTime(2022, 1, 10, 0, 8, 0, 0, DateTimeKind.Unspecified), -100.0 },
                    { new Guid("f42e3870-36ef-4e57-895a-86cc572823f5"), new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"), new DateTime(2022, 1, 24, 0, 12, 0, 0, DateTimeKind.Unspecified), 20.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: new Guid("10030d26-c959-425e-87fe-f54b570d44eb"));

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: new Guid("18f9a75b-d766-4f4a-9347-6bafc0e5d4e6"));

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: new Guid("f42e3870-36ef-4e57-895a-86cc572823f5"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("e4c91720-b231-45a4-80a0-85a7f2034891"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("fe024929-3699-40b0-ab21-c619b1f83721"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c6bcb46f-f210-4dcc-8e3f-ac66d9ad9cc4"));

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
    }
}
