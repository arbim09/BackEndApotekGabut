using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNamingBarangDisplaypart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangDisplays_Satuans_SatuanJualId",
                table: "BarangDisplays");

            migrationBuilder.DropIndex(
                name: "IX_BarangDisplays_SatuanJualId",
                table: "BarangDisplays");

            migrationBuilder.DropColumn(
                name: "SatuanJualId",
                table: "BarangDisplays");

            migrationBuilder.CreateIndex(
                name: "IX_BarangDisplays_Satuan_jual_id",
                table: "BarangDisplays",
                column: "Satuan_jual_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangDisplays_Satuans_Satuan_jual_id",
                table: "BarangDisplays",
                column: "Satuan_jual_id",
                principalTable: "Satuans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangDisplays_Satuans_Satuan_jual_id",
                table: "BarangDisplays");

            migrationBuilder.DropIndex(
                name: "IX_BarangDisplays_Satuan_jual_id",
                table: "BarangDisplays");

            migrationBuilder.AddColumn<int>(
                name: "SatuanJualId",
                table: "BarangDisplays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarangDisplays_SatuanJualId",
                table: "BarangDisplays",
                column: "SatuanJualId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangDisplays_Satuans_SatuanJualId",
                table: "BarangDisplays",
                column: "SatuanJualId",
                principalTable: "Satuans",
                principalColumn: "Id");
        }
    }
}
