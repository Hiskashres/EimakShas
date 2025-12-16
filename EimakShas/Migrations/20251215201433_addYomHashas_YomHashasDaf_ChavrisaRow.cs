using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class addYomHashas_YomHashasDaf_ChavrisaRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChavrisaId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "YomHashas",
                columns: table => new
                {
                    YomHashasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafimCompleted = table.Column<int>(type: "int", nullable: false),
                    PercentCompleted = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    BonusGoal = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YomHashas", x => x.YomHashasId);
                });

            migrationBuilder.CreateTable(
                name: "YomHashas_Dafim",
                columns: table => new
                {
                    YomHashas_DafId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YomHashas_Dafim", x => x.YomHashas_DafId);
                    table.ForeignKey(
                        name: "FK_YomHashas_Dafim_Dafim_DafId",
                        column: x => x.DafId,
                        principalTable: "Dafim",
                        principalColumn: "DafId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserYomHashas_Daf",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    yomHashas_DafimYomHashas_DafId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserYomHashas_Daf", x => new { x.UsersUserId, x.yomHashas_DafimYomHashas_DafId });
                    table.ForeignKey(
                        name: "FK_UserYomHashas_Daf_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserYomHashas_Daf_YomHashas_Dafim_yomHashas_DafimYomHashas_DafId",
                        column: x => x.yomHashas_DafimYomHashas_DafId,
                        principalTable: "YomHashas_Dafim",
                        principalColumn: "YomHashas_DafId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChavrisaId",
                table: "Users",
                column: "ChavrisaId",
                unique: true,
                filter: "[ChavrisaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserYomHashas_Daf_yomHashas_DafimYomHashas_DafId",
                table: "UserYomHashas_Daf",
                column: "yomHashas_DafimYomHashas_DafId");

            migrationBuilder.CreateIndex(
                name: "IX_YomHashas_Dafim_DafId",
                table: "YomHashas_Dafim",
                column: "DafId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ChavrisaId",
                table: "Users",
                column: "ChavrisaId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ChavrisaId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserYomHashas_Daf");

            migrationBuilder.DropTable(
                name: "YomHashas");

            migrationBuilder.DropTable(
                name: "YomHashas_Dafim");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChavrisaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChavrisaId",
                table: "Users");
        }
    }
}
