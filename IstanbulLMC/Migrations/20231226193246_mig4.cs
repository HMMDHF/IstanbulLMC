using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IstanbulLMC.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "Transfer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RoundTripDate",
                schema: "dbo",
                table: "Transfer",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "RoundTripDate",
                schema: "dbo",
                table: "Transfer");
        }
    }
}
