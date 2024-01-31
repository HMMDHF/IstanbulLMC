using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IstanbulLMC.Migrations
{
    /// <inheritdoc />
    public partial class Updatedcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TlUnit = table.Column<double>(type: "float", nullable: false),
                    UsdUnit = table.Column<double>(type: "float", nullable: false),
                    EuroUnit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency",
                schema: "dbo");
        }
    }
}
