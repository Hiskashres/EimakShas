using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class addDafimFinished_PercentageFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DafimFinished",
                table: "Masechtas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageFinished",
                table: "Masechtas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DafimFinished",
                table: "Masechtas");

            migrationBuilder.DropColumn(
                name: "PercentageFinished",
                table: "Masechtas");
        }
    }
}
