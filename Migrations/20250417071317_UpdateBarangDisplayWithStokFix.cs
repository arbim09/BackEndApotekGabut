using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBarangDisplayWithStokFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jumlah_display",
                table: "BarangDisplays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jumlah_display",
                table: "BarangDisplays",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
