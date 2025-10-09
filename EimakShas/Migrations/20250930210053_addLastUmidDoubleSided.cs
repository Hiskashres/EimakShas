using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EimakShas.Migrations
{
    /// <inheritdoc />
    public partial class addLastUmidDoubleSided : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DafNumber",
                table: "Dafim",
                newName: "DafLetter");

            migrationBuilder.AddColumn<bool>(
                name: "LastUmidDoubleSided",
                table: "Masechtas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUmidDoubleSided",
                table: "Masechtas");

            migrationBuilder.RenameColumn(
                name: "DafLetter",
                table: "Dafim",
                newName: "DafNumber");
        }
    }
}
