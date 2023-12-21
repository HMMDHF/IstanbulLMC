using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IstanbulLMC.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferServices_ServiceID",
                schema: "dbo",
                table: "TransferServices",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferServices_Service_ServiceID",
                schema: "dbo",
                table: "TransferServices",
                column: "ServiceID",
                principalSchema: "dbo",
                principalTable: "Service",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferServices_Service_ServiceID",
                schema: "dbo",
                table: "TransferServices");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_TransferServices_ServiceID",
                schema: "dbo",
                table: "TransferServices");
        }
    }
}
