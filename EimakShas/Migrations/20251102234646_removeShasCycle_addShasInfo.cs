using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class removeShasCycle_addShasInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masechtas_ShasCycles_ShasCycleId",
                table: "Masechtas");

            migrationBuilder.DropTable(
                name: "ShasCycles");

            migrationBuilder.DropIndex(
                name: "IX_Masechtas_ShasCycleId",
                table: "Masechtas");

            migrationBuilder.DropColumn(
                name: "ShasCycleId",
                table: "Masechtas");

            migrationBuilder.CreateTable(
                name: "ShasInfo",
                columns: table => new
                {
                    ShasInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafimCount_Shas = table.Column<int>(type: "int", nullable: false),
                    ShasPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasInfo", x => x.ShasInfoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShasInfo");

            migrationBuilder.AddColumn<int>(
                name: "ShasCycleId",
                table: "Masechtas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShasCycles",
                columns: table => new
                {
                    ShasCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShasCycleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasCycles", x => x.ShasCycleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masechtas_ShasCycleId",
                table: "Masechtas",
                column: "ShasCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Masechtas_ShasCycles_ShasCycleId",
                table: "Masechtas",
                column: "ShasCycleId",
                principalTable: "ShasCycles",
                principalColumn: "ShasCycleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
