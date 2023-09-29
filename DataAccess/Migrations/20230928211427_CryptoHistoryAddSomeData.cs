using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CryptoHistoryAddSomeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CryptoHistories",
                columns: new[] { "Id", "BaseCurrencyCode", "CreatedAt", "CreatedBy", "CryptoCurrencyCode", "IsActive", "IsDeleted", "Rate", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "USD", new DateTime(2023, 9, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1796), 0, "BTC", true, false, 15000m, new DateTime(2023, 9, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1811), 0 },
                    { 2, "USD", new DateTime(2023, 9, 28, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1833), 0, "BTC", true, false, 10000m, new DateTime(2023, 9, 28, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1838), 0 },
                    { 3, "USD", new DateTime(2023, 9, 22, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1845), 0, "BTC", true, false, 25000m, new DateTime(2023, 9, 22, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1847), 0 },
                    { 4, "USD", new DateTime(2023, 8, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1852), 0, "BTC", true, false, 5000m, new DateTime(2023, 8, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1856), 0 },
                    { 5, "USD", new DateTime(2023, 7, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1863), 0, "BTC", true, false, 7500m, new DateTime(2023, 7, 29, 0, 14, 27, 146, DateTimeKind.Local).AddTicks(1864), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CryptoHistories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CryptoHistories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CryptoHistories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CryptoHistories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CryptoHistories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
