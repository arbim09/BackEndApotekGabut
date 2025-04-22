using Apotek.Data;
using Apotek.Models;

namespace Apotek.Seeder
{
    public static class KategoriBarangSeeder
    {
        public static void SeedKategoriBarang(DataContext context)
        {
            if (!context.KategoriBarangs.Any())
            {
                var now = DateTime.UtcNow;
                var kategoriList = new List<KategoriBarang>
        {
            new() { Nama_kategori = "Antibiotik", CreatedAt = now },
            new() { Nama_kategori = "Vitamin & Suplemen", CreatedAt = now },
            new() { Nama_kategori = "Obat Batuk & Flu", CreatedAt = now },
            new() { Nama_kategori = "Obat Demam", CreatedAt = now },
            new() { Nama_kategori = "Obat Pencernaan", CreatedAt = now },
            new() { Nama_kategori = "Obat Kulit", CreatedAt = now },
            new() { Nama_kategori = "Obat Mata", CreatedAt = now },
            new() { Nama_kategori = "Obat Tetes Telinga", CreatedAt = now },
            new() { Nama_kategori = "Obat Luka", CreatedAt = now },
            new() { Nama_kategori = "Obat Alergi", CreatedAt = now },
            new() { Nama_kategori = "Obat Sakit Kepala", CreatedAt = now },
            new() { Nama_kategori = "Obat Nyeri Otot", CreatedAt = now },
            new() { Nama_kategori = "Obat Tidur", CreatedAt = now },
            new() { Nama_kategori = "Obat Asam Urat", CreatedAt = now },
            new() { Nama_kategori = "Obat Asam Lambung", CreatedAt = now },
            new() { Nama_kategori = "Obat Hipertensi", CreatedAt = now },
            new() { Nama_kategori = "Obat Diabetes", CreatedAt = now },
            new() { Nama_kategori = "Obat Jantung", CreatedAt = now },
            new() { Nama_kategori = "Obat Herbal", CreatedAt = now },
            new() { Nama_kategori = "Alat Kesehatan", CreatedAt = now },
            new() { Nama_kategori = "Masker", CreatedAt = now },
            new() { Nama_kategori = "Hand Sanitizer", CreatedAt = now },
            new() { Nama_kategori = "Alkohol & Antiseptik", CreatedAt = now },
            new() { Nama_kategori = "Perban & Plester", CreatedAt = now },
            new() { Nama_kategori = "Test Kehamilan", CreatedAt = now },
            new() { Nama_kategori = "Test Gula Darah", CreatedAt = now },
            new() { Nama_kategori = "Minyak Angin & Balsem", CreatedAt = now },
            new() { Nama_kategori = "Obat Maag", CreatedAt = now },
            new() { Nama_kategori = "Obat Kontrasepsi", CreatedAt = now },
            new() { Nama_kategori = "Obat Pelancar Haid", CreatedAt = now }
        };

                context.KategoriBarangs.AddRange(kategoriList);
                context.SaveChanges();
            }
        }

    }
}
