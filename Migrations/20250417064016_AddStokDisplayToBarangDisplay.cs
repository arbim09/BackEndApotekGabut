using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class AddStokDisplayToBarangDisplay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stok_display",
                table: "BarangDisplays",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stok_display",
                table: "BarangDisplays");
        }
    }
}
