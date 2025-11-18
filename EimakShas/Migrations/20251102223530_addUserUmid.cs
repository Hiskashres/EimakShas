using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class addUserUmid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Umidim_Users_UserId",
                table: "Umidim");

            migrationBuilder.DropIndex(
                name: "IX_Umidim_UserId",
                table: "Umidim");

            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Umidim",
                newName: "IsCompleted_Umid");

            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Dafim",
                newName: "IsCompleted_Daf");

            migrationBuilder.CreateTable(
                name: "UserUmidim",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UmidId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted_UserUmid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUmidim", x => new { x.UserId, x.UmidId });
                    table.ForeignKey(
                        name: "FK_UserUmidim_Umidim_UmidId",
                        column: x => x.UmidId,
                        principalTable: "Umidim",
                        principalColumn: "UmidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUmidim_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUmidim_UmidId",
                table: "UserUmidim",
                column: "UmidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUmidim");

            migrationBuilder.RenameColumn(
                name: "IsCompleted_Umid",
                table: "Umidim",
                newName: "IsCompleted");

            migrationBuilder.RenameColumn(
                name: "IsCompleted_Daf",
                table: "Dafim",
                newName: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_Umidim_UserId",
                table: "Umidim",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Umidim_Users_UserId",
                table: "Umidim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
