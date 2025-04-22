using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangGudangs_KategoriBarangs_KategoriId",
                table: "BarangGudangs");

            migrationBuilder.DropForeignKey(
                name: "FK_BarangGudangs_Satuans_SatuanMasukId",
                table: "BarangGudangs");

            migrationBuilder.DropIndex(
                name: "IX_BarangGudangs_KategoriId",
                table: "BarangGudangs");

            migrationBuilder.DropIndex(
                name: "IX_BarangGudangs_SatuanMasukId",
                table: "BarangGudangs");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "BarangGudangs");

            migrationBuilder.DropColumn(
                name: "SatuanMasukId",
                table: "BarangGudangs");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_Kategori_id",
                table: "BarangGudangs",
                column: "Kategori_id");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_Satuan_masuk_id",
                table: "BarangGudangs",
                column: "Satuan_masuk_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangGudangs_KategoriBarangs_Kategori_id",
                table: "BarangGudangs",
                column: "Kategori_id",
                principalTable: "KategoriBarangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarangGudangs_Satuans_Satuan_masuk_id",
                table: "BarangGudangs",
                column: "Satuan_masuk_id",
                principalTable: "Satuans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangGudangs_KategoriBarangs_Kategori_id",
                table: "BarangGudangs");

            migrationBuilder.DropForeignKey(
                name: "FK_BarangGudangs_Satuans_Satuan_masuk_id",
                table: "BarangGudangs");

            migrationBuilder.DropIndex(
                name: "IX_BarangGudangs_Kategori_id",
                table: "BarangGudangs");

            migrationBuilder.DropIndex(
                name: "IX_BarangGudangs_Satuan_masuk_id",
                table: "BarangGudangs");

            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "BarangGudangs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SatuanMasukId",
                table: "BarangGudangs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_KategoriId",
                table: "BarangGudangs",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_SatuanMasukId",
                table: "BarangGudangs",
                column: "SatuanMasukId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangGudangs_KategoriBarangs_KategoriId",
                table: "BarangGudangs",
                column: "KategoriId",
                principalTable: "KategoriBarangs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangGudangs_Satuans_SatuanMasukId",
                table: "BarangGudangs",
                column: "SatuanMasukId",
                principalTable: "Satuans",
                principalColumn: "Id");
        }
    }
}
