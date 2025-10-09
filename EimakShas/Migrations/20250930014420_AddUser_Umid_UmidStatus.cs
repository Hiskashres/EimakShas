using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class AddUser_Umid_UmidStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Daf_Masechta_MasechtaId",
                table: "Daf");

            migrationBuilder.DropForeignKey(
                name: "FK_Masechta_ShasCycle_ShasCycleId",
                table: "Masechta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShasCycle",
                table: "ShasCycle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Masechta",
                table: "Masechta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Daf",
                table: "Daf");

            migrationBuilder.DropColumn(
                name: "Umid",
                table: "Daf");

            migrationBuilder.RenameTable(
                name: "ShasCycle",
                newName: "ShasCycles");

            migrationBuilder.RenameTable(
                name: "Masechta",
                newName: "Masechtas");

            migrationBuilder.RenameTable(
                name: "Daf",
                newName: "Dafim");

            migrationBuilder.RenameIndex(
                name: "IX_Masechta_ShasCycleId",
                table: "Masechtas",
                newName: "IX_Masechtas_ShasCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_Daf_MasechtaId",
                table: "Dafim",
                newName: "IX_Dafim_MasechtaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShasCycles",
                table: "ShasCycles",
                column: "ShasCycleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Masechtas",
                table: "Masechtas",
                column: "MasechtaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dafim",
                table: "Dafim",
                column: "DafId");

            migrationBuilder.CreateTable(
                name: "Umidim",
                columns: table => new
                {
                    UmidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafId = table.Column<int>(type: "int", nullable: false),
                    UmidLetter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Umidim", x => x.UmidId);
                    table.ForeignKey(
                        name: "FK_Umidim_Dafim_DafId",
                        column: x => x.DafId,
                        principalTable: "Dafim",
                        principalColumn: "DafId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UmidimStatus",
                columns: table => new
                {
                    UmidStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmidId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UmidimStatus", x => x.UmidStatusId);
                    table.ForeignKey(
                        name: "FK_UmidimStatus_Umidim_UmidId",
                        column: x => x.UmidId,
                        principalTable: "Umidim",
                        principalColumn: "UmidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UmidimStatus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Umidim_DafId",
                table: "Umidim",
                column: "DafId");

            migrationBuilder.CreateIndex(
                name: "IX_UmidimStatus_UmidId",
                table: "UmidimStatus",
                column: "UmidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UmidimStatus_UserId",
                table: "UmidimStatus",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dafim_Masechtas_MasechtaId",
                table: "Dafim",
                column: "MasechtaId",
                principalTable: "Masechtas",
                principalColumn: "MasechtaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Masechtas_ShasCycles_ShasCycleId",
                table: "Masechtas",
                column: "ShasCycleId",
                principalTable: "ShasCycles",
                principalColumn: "ShasCycleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dafim_Masechtas_MasechtaId",
                table: "Dafim");

            migrationBuilder.DropForeignKey(
                name: "FK_Masechtas_ShasCycles_ShasCycleId",
                table: "Masechtas");

            migrationBuilder.DropTable(
                name: "UmidimStatus");

            migrationBuilder.DropTable(
                name: "Umidim");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShasCycles",
                table: "ShasCycles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Masechtas",
                table: "Masechtas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dafim",
                table: "Dafim");

            migrationBuilder.RenameTable(
                name: "ShasCycles",
                newName: "ShasCycle");

            migrationBuilder.RenameTable(
                name: "Masechtas",
                newName: "Masechta");

            migrationBuilder.RenameTable(
                name: "Dafim",
                newName: "Daf");

            migrationBuilder.RenameIndex(
                name: "IX_Masechtas_ShasCycleId",
                table: "Masechta",
                newName: "IX_Masechta_ShasCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_Dafim_MasechtaId",
                table: "Daf",
                newName: "IX_Daf_MasechtaId");

            migrationBuilder.AddColumn<string>(
                name: "Umid",
                table: "Daf",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShasCycle",
                table: "ShasCycle",
                column: "ShasCycleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Masechta",
                table: "Masechta",
                column: "MasechtaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Daf",
                table: "Daf",
                column: "DafId");

            migrationBuilder.AddForeignKey(
                name: "FK_Daf_Masechta_MasechtaId",
                table: "Daf",
                column: "MasechtaId",
                principalTable: "Masechta",
                principalColumn: "MasechtaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Masechta_ShasCycle_ShasCycleId",
                table: "Masechta",
                column: "ShasCycleId",
                principalTable: "ShasCycle",
                principalColumn: "ShasCycleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
