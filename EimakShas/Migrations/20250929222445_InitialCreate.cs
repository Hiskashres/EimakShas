using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShasCycle",
                columns: table => new
                {
                    ShasCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShasCycleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasCycle", x => x.ShasCycleId);
                });

            migrationBuilder.CreateTable(
                name: "Masechta",
                columns: table => new
                {
                    MasechtaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShasCycleId = table.Column<int>(type: "int", nullable: false),
                    MasechtaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasechtaDafCount = table.Column<int>(type: "int", nullable: false),
                    MasechtaOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masechta", x => x.MasechtaId);
                    table.ForeignKey(
                        name: "FK_Masechta_ShasCycle_ShasCycleId",
                        column: x => x.ShasCycleId,
                        principalTable: "ShasCycle",
                        principalColumn: "ShasCycleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Daf",
                columns: table => new
                {
                    DafId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasechtaId = table.Column<int>(type: "int", nullable: false),
                    DafNumber = table.Column<int>(type: "int", nullable: false),
                    Umid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daf", x => x.DafId);
                    table.ForeignKey(
                        name: "FK_Daf_Masechta_MasechtaId",
                        column: x => x.MasechtaId,
                        principalTable: "Masechta",
                        principalColumn: "MasechtaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Daf_MasechtaId",
                table: "Daf",
                column: "MasechtaId");

            migrationBuilder.CreateIndex(
                name: "IX_Masechta_ShasCycleId",
                table: "Masechta",
                column: "ShasCycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Daf");

            migrationBuilder.DropTable(
                name: "Masechta");

            migrationBuilder.DropTable(
                name: "ShasCycle");
        }
    }
}
