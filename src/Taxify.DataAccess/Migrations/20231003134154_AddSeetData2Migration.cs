using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taxify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeetData2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 3, 13, 41, 54, 514, DateTimeKind.Utc).AddTicks(5412), "$2a$11$DlirZW4x/I7dUfc.DJwFiuUOIqKbpwKrdb8sCdOA10guTXv2PebAq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 3, 13, 25, 30, 544, DateTimeKind.Utc).AddTicks(1640), "12345" });
        }
    }
}
