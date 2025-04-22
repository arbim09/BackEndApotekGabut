using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class BarangGudang
    {
        public int Id { get; set; }
        public string Nama_barang { get; set; }
        public int Kategori_id { get; set; }
        [ForeignKey("Kategori_id")]
        public KategoriBarang? Kategori { get; set; }
        public int Satuan_masuk_id { get; set; }
        [ForeignKey("Satuan_masuk_id")]
        public Satuan? SatuanMasuk { get; set; }
        public decimal Harga_beli { get; set; }
        public int Jumlah_per_satuan { get; set; } // default per masuk
        public int Stok { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}