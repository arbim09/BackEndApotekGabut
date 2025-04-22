using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class BarangDisplay
    {
        public int Id { get; set; }

        public int BarangGudangId { get; set; }
        public BarangGudang? BarangGudang { get; set; }

        public string Nama_display { get; set; } // Contoh: "Obat A - Pcs"

        public int Satuan_jual_id { get; set; }
        [ForeignKey("Satuan_jual_id")]
        public Satuan? SatuanJual { get; set; }
        public int Konversi_dari_gudang { get; set; } // Contoh: 1 box = 10 pcs → nilai = 10
        public decimal Harga_jual { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Stok_display { get; set; }
        //public int Jumlah_display { get; set; }// Hapus [NotMapped]

    }

}