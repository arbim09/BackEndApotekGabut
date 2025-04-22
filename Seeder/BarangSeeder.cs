using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Data;
using Apotek.Models;

namespace Apotek.Seeder
{
    public class BarangSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.KategoriBarangs.Any())
            {
                var now = DateTime.UtcNow;
                var kategoriList = new List<KategoriBarang>
            {
                new() { Id = 1, Nama_kategori = "Antibiotik", CreatedAt = now },
                new() { Id = 2, Nama_kategori = "Vitamin & Suplemen", CreatedAt = now },
                new() { Id = 3, Nama_kategori = "Obat Batuk & Flu", CreatedAt = now },
                new() { Id = 4, Nama_kategori = "Obat Demam", CreatedAt = now },
                new() { Id = 5, Nama_kategori = "Obat Pencernaan", CreatedAt = now },
                new() { Id = 6, Nama_kategori = "Obat Kulit", CreatedAt = now },
                new() { Id = 7, Nama_kategori = "Obat Mata", CreatedAt = now },
                new() { Id = 8, Nama_kategori = "Obat Tetes Telinga", CreatedAt = now },
                new() { Id = 9, Nama_kategori = "Obat Luka", CreatedAt = now },
                new() { Id = 10, Nama_kategori = "Obat Alergi", CreatedAt = now },
                new() { Id = 11, Nama_kategori = "Obat Sakit Kepala", CreatedAt = now },
                new() { Id = 12, Nama_kategori = "Obat Nyeri Otot", CreatedAt = now },
                new() { Id = 13, Nama_kategori = "Obat Tidur", CreatedAt = now },
                new() { Id = 14, Nama_kategori = "Obat Asam Urat", CreatedAt = now },
                new() { Id = 15, Nama_kategori = "Obat Asam Lambung", CreatedAt = now },
                new() { Id = 16, Nama_kategori = "Obat Hipertensi", CreatedAt = now },
                new() { Id = 17, Nama_kategori = "Obat Diabetes", CreatedAt = now },
                new() { Id = 18, Nama_kategori = "Obat Jantung", CreatedAt = now },
                new() { Id = 19, Nama_kategori = "Obat Herbal", CreatedAt = now },
                new() { Id = 20, Nama_kategori = "Alat Kesehatan", CreatedAt = now },
                new() { Id = 21, Nama_kategori = "Masker", CreatedAt = now },
                new() { Id = 22, Nama_kategori = "Hand Sanitizer", CreatedAt = now },
                new() { Id = 23, Nama_kategori = "Alkohol & Antiseptik", CreatedAt = now },
                new() { Id = 24, Nama_kategori = "Perban & Plester", CreatedAt = now },
                new() { Id = 25, Nama_kategori = "Test Kehamilan", CreatedAt = now },
                new() { Id = 26, Nama_kategori = "Test Gula Darah", CreatedAt = now },
                new() { Id = 27, Nama_kategori = "Minyak Angin & Balsem", CreatedAt = now },
                new() { Id = 28, Nama_kategori = "Obat Maag", CreatedAt = now },
                new() { Id = 29, Nama_kategori = "Obat Kontrasepsi", CreatedAt = now },
                new() { Id = 30, Nama_kategori = "Obat Pelancar Haid", CreatedAt = now }
            };
                context.KategoriBarangs.AddRange(kategoriList);
                context.SaveChanges();
            }

            if (!context.BarangGudangs.Any())
            {
                var now = DateTime.UtcNow;
                var kategoriList = context.KategoriBarangs.OrderBy(x => x.Id).Take(20).ToList();
                var satuanList = context.Satuans.OrderBy(x => x.Id).Take(20).ToList();

                var barangGudangList = new List<BarangGudang>();
                var barangDisplayList = new List<BarangDisplay>();

                for (int i = 0; i < satuanList.Count && i < kategoriList.Count; i++)
                {
                    var masuk = satuanList[i];
                    var jual = satuanList[(i + 1) % satuanList.Count];
                    var kategori = kategoriList[i];

                    var gudang = new BarangGudang
                    {
                        Nama_barang = $"Obat {i + 1}",
                        Kategori_id = kategori.Id,
                        Satuan_masuk_id = masuk.Id,
                        Harga_beli = 10000 + ((i + 1) * 1000),
                        Jumlah_per_satuan = 10,
                        Stok = 100,
                        CreatedAt = now
                    };

                    barangGudangList.Add(gudang);
                    context.BarangGudangs.Add(gudang);
                    context.SaveChanges();

                    var display = new BarangDisplay
                    {
                        BarangGudangId = gudang.Id,
                        Nama_display = $"{gudang.Nama_barang} - {jual.Name}",
                        Satuan_jual_id = jual.Id,
                        Konversi_dari_gudang = 10,
                        Harga_jual = gudang.Harga_beli + 3000,
                        CreatedAt = now
                    };

                    barangDisplayList.Add(display);
                }

                context.BarangDisplays.AddRange(barangDisplayList);
                context.SaveChanges();
            }
        }
    }
}