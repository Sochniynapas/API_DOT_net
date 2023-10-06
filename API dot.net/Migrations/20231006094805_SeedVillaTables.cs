using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_dot.net.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedDate", "ImageUrl", "Name", "Squares", "UpdateDate", "Сapacity" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://unsplash.com/photos/2pPw5Glro5I", "Royall Villa", 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://unsplash.com/photos/2pPw5Glro5I", "Medium Villa", 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://unsplash.com/photos/2pPw5Glro5I", "Low Villa", 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
