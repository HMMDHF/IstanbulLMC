using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IstanbulLMC.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DriverTrip",
                schema: "dbo",
                table: "Transfer",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverTrip",
                schema: "dbo",
                table: "Transfer");
        }
    }
}
