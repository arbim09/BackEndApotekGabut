using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class AddModelMutasiBarangDisplay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiubahOleh",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keterangan",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StokMutasiBarangDisplays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BarangDisplayId = table.Column<int>(type: "integer", nullable: false),
                    TipeMutasi = table.Column<string>(type: "text", nullable: false),
                    JumlahKonversi = table.Column<int>(type: "integer", nullable: false),
                    JumlahStokDisplay = table.Column<int>(type: "integer", nullable: false),
                    StokGudangSaatItu = table.Column<int>(type: "integer", nullable: false),
                    WaktuMutasi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokMutasiBarangDisplays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokMutasiBarangDisplays_BarangDisplays_BarangDisplayId",
                        column: x => x.BarangDisplayId,
                        principalTable: "BarangDisplays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StokMutasiBarangDisplays_BarangDisplayId",
                table: "StokMutasiBarangDisplays",
                column: "BarangDisplayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StokMutasiBarangDisplays");

            migrationBuilder.DropColumn(
                name: "DiubahOleh",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Keterangan",
                table: "Users");
        }
    }
}
