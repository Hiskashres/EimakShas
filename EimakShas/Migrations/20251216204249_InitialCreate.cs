using System;
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
                name: "Masechtas",
                columns: table => new
                {
                    MasechtaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasechtaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasechtaDafCount = table.Column<int>(type: "int", nullable: false),
                    MasechtaOrder = table.Column<int>(type: "int", nullable: false),
                    LastUmidDoubleSided = table.Column<bool>(type: "bit", nullable: false),
                    DafimFinished = table.Column<int>(type: "int", nullable: false),
                    PercentageFinished = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masechtas", x => x.MasechtaId);
                });

            migrationBuilder.CreateTable(
                name: "ShasInfo",
                columns: table => new
                {
                    ShasInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafimCount_Shas = table.Column<int>(type: "int", nullable: false),
                    DafimFinished = table.Column<int>(type: "int", nullable: false),
                    DafimLearned = table.Column<int>(type: "int", nullable: false),
                    PercentageFinished = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShasInfo", x => x.ShasInfoId);
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
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChavrisaId = table.Column<int>(type: "int", nullable: true),
                    DafimAmount = table.Column<int>(type: "int", nullable: false),
                    DafimFinished = table.Column<int>(type: "int", nullable: false),
                    PercentageFinished = table.Column<int>(type: "int", nullable: false),
                    DafPerDay = table.Column<bool>(type: "bit", nullable: false),
                    HasText = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Users_ChavrisaId",
                        column: x => x.ChavrisaId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "YomHashas",
                columns: table => new
                {
                    YomHashasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafimCompleted = table.Column<int>(type: "int", nullable: false),
                    PercentCompleted = table.Column<int>(type: "int", nullable: false),
                    PercentCompleted_Bonus = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    BonusGoal = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YomHashas", x => x.YomHashasId);
                });

            migrationBuilder.CreateTable(
                name: "Dafim",
                columns: table => new
                {
                    DafId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasechtaId = table.Column<int>(type: "int", nullable: false),
                    DafLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DafNumber = table.Column<int>(type: "int", nullable: false),
                    IsCompleted_Daf = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dafim", x => x.DafId);
                    table.ForeignKey(
                        name: "FK_Dafim_Masechtas_MasechtaId",
                        column: x => x.MasechtaId,
                        principalTable: "Masechtas",
                        principalColumn: "MasechtaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Umidim",
                columns: table => new
                {
                    UmidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DafId = table.Column<int>(type: "int", nullable: false),
                    UmidLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted_Umid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_Dafim_MasechtaId",
                table: "Dafim",
                column: "MasechtaId");

            migrationBuilder.CreateIndex(
                name: "IX_Umidim_DafId",
                table: "Umidim",
                column: "DafId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChavrisaId",
                table: "Users",
                column: "ChavrisaId",
                unique: true,
                filter: "[ChavrisaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserUmidim_UmidId",
                table: "UserUmidim",
                column: "UmidId");

            migrationBuilder.CreateIndex(
                name: "IX_UserYomHashas_Daf_yomHashas_DafimYomHashas_DafId",
                table: "UserYomHashas_Daf",
                column: "yomHashas_DafimYomHashas_DafId");

            migrationBuilder.CreateIndex(
                name: "IX_YomHashas_Dafim_DafId",
                table: "YomHashas_Dafim",
                column: "DafId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShasInfo");

            migrationBuilder.DropTable(
                name: "UserUmidim");

            migrationBuilder.DropTable(
                name: "UserYomHashas_Daf");

            migrationBuilder.DropTable(
                name: "YomHashas");

            migrationBuilder.DropTable(
                name: "Umidim");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "YomHashas_Dafim");

            migrationBuilder.DropTable(
                name: "Dafim");

            migrationBuilder.DropTable(
                name: "Masechtas");
        }
    }
}
