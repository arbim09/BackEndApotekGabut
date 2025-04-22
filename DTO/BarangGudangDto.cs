using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.DTO
{
    public class BarangGudangDto
    {
        public int Id { get; set; }
        public string Nama_barang { get; set; } = string.Empty;
        public int Kategori_id { get; set; }
        public int Satuan_masuk_id { get; set; }
        public decimal Harga_beli { get; set; }
        public int Jumlah_per_satuan { get; set; }
        public int Stok { get; set; }
    }
}