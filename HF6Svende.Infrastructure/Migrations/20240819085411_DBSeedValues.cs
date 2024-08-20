using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HF6Svende.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBSeedValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryCode", "Name" },
                values: new object[,]
                {
                    { 2, "SE", "Sweden" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "GenderId", "Name" },
                values: new object[] { 1, 1, "Watches" });


            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "Id", "Address", "CountryId", "DeliveredDate", "DeliveryFee", "DispatchedDate", "EstDeliveryDate", "PostalCodeId" },
                values: new object[] { 1, "gogade 22", 1, new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc), 30m, new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc), new DateTime(2024, 8, 16, 10, 9, 15, 433, DateTimeKind.Utc), 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CustomerId", "Method" },
                values: new object[] { 1, 1000m, 1, "Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
