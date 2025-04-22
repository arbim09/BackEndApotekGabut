using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSatuanJualInBarangGudangs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangGudangs_Satuans_SatuanJualId",
                table: "BarangGudangs");

            migrationBuilder.DropIndex(
                name: "IX_BarangGudangs_SatuanJualId",
                table: "BarangGudangs");

            migrationBuilder.DropColumn(
                name: "SatuanJualId",
                table: "BarangGudangs");

            migrationBuilder.DropColumn(
                name: "Satuan_jual_id",
                table: "BarangGudangs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SatuanJualId",
                table: "BarangGudangs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Satuan_jual_id",
                table: "BarangGudangs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_SatuanJualId",
                table: "BarangGudangs",
                column: "SatuanJualId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangGudangs_Satuans_SatuanJualId",
                table: "BarangGudangs",
                column: "SatuanJualId",
                principalTable: "Satuans",
                principalColumn: "Id");
        }
    }
}
