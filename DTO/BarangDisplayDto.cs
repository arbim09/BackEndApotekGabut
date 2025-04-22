using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.DTO
{
    public class BarangDisplayDto
    {
        public int Id { get; set; }
        public int BarangGudangId { get; set; }
        public string Nama_display { get; set; } = string.Empty;
        public int Satuan_jual_id { get; set; }
        public int Konversi_dari_gudang { get; set; }
        public decimal Harga_jual { get; set; }
        public int Stok_display { get; set; }
        // public int Jumlah_display { get; set; }
    }

}