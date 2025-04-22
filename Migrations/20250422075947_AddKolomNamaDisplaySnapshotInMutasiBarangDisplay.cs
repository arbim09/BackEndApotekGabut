using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class AddKolomNamaDisplaySnapshotInMutasiBarangDisplay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NamaDisplaySnapshot",
                table: "StokMutasiBarangDisplays",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamaDisplaySnapshot",
                table: "StokMutasiBarangDisplays");
        }
    }
}
