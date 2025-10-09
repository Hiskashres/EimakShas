using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUmidStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UmidimStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Umidim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Umidim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Umidim_UserId",
                table: "Umidim",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Umidim_Users_UserId",
                table: "Umidim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Umidim_Users_UserId",
                table: "Umidim");

            migrationBuilder.DropIndex(
                name: "IX_Umidim_UserId",
                table: "Umidim");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Umidim");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Umidim");

            migrationBuilder.CreateTable(
                name: "UmidimStatus",
                columns: table => new
                {
                    UmidStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmidId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_UmidimStatus_UmidId",
                table: "UmidimStatus",
                column: "UmidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UmidimStatus_UserId",
                table: "UmidimStatus",
                column: "UserId");
        }
    }
}
