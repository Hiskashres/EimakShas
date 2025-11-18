using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class addDafimAmount_DafimFinished_PercentageFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DafimAmount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DafimFinished",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageFinished",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DafimAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DafimFinished",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PercentageFinished",
                table: "Users");
        }
    }
}
