using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Apotek.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarangMasuks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tanggal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Supplier_id = table.Column<int>(type: "integer", nullable: false),
                    User_id = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangMasuks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailBarangMasuks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Barang_masuk_id = table.Column<int>(type: "integer", nullable: false),
                    Barang_id = table.Column<int>(type: "integer", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false),
                    Harga_beli = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailBarangMasuks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailPenjualans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Penjualan_id = table.Column<int>(type: "integer", nullable: false),
                    Barang_id = table.Column<int>(type: "integer", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false),
                    Harga_jual = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPenjualans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KategoriBarangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama_kategori = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriBarangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Penjualans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tanggal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    User_id = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penjualans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satuans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satuans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Alamat = table.Column<string>(type: "text", nullable: false),
                    No_hp = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarangGudangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama_barang = table.Column<string>(type: "text", nullable: false),
                    Kategori_id = table.Column<int>(type: "integer", nullable: false),
                    KategoriId = table.Column<int>(type: "integer", nullable: true),
                    Satuan_masuk_id = table.Column<int>(type: "integer", nullable: false),
                    SatuanMasukId = table.Column<int>(type: "integer", nullable: true),
                    Satuan_jual_id = table.Column<int>(type: "integer", nullable: false),
                    SatuanJualId = table.Column<int>(type: "integer", nullable: true),
                    Harga_beli = table.Column<decimal>(type: "numeric", nullable: false),
                    Jumlah_per_satuan = table.Column<int>(type: "integer", nullable: false),
                    Stok = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangGudangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarangGudangs_KategoriBarangs_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "KategoriBarangs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BarangGudangs_Satuans_SatuanJualId",
                        column: x => x.SatuanJualId,
                        principalTable: "Satuans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BarangGudangs_Satuans_SatuanMasukId",
                        column: x => x.SatuanMasukId,
                        principalTable: "Satuans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BarangDisplays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BarangGudangId = table.Column<int>(type: "integer", nullable: false),
                    Nama_display = table.Column<string>(type: "text", nullable: false),
                    Satuan_jual_id = table.Column<int>(type: "integer", nullable: false),
                    SatuanJualId = table.Column<int>(type: "integer", nullable: true),
                    Konversi_dari_gudang = table.Column<int>(type: "integer", nullable: false),
                    Harga_jual = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangDisplays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarangDisplays_BarangGudangs_BarangGudangId",
                        column: x => x.BarangGudangId,
                        principalTable: "BarangGudangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarangDisplays_Satuans_SatuanJualId",
                        column: x => x.SatuanJualId,
                        principalTable: "Satuans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarangDisplays_BarangGudangId",
                table: "BarangDisplays",
                column: "BarangGudangId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangDisplays_SatuanJualId",
                table: "BarangDisplays",
                column: "SatuanJualId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_KategoriId",
                table: "BarangGudangs",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_SatuanJualId",
                table: "BarangGudangs",
                column: "SatuanJualId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangGudangs_SatuanMasukId",
                table: "BarangGudangs",
                column: "SatuanMasukId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarangDisplays");

            migrationBuilder.DropTable(
                name: "BarangMasuks");

            migrationBuilder.DropTable(
                name: "DetailBarangMasuks");

            migrationBuilder.DropTable(
                name: "DetailPenjualans");

            migrationBuilder.DropTable(
                name: "Penjualans");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BarangGudangs");

            migrationBuilder.DropTable(
                name: "KategoriBarangs");

            migrationBuilder.DropTable(
                name: "Satuans");
        }
    }
}
