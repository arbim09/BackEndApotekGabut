using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class StokMutasiBarangDisplay
    {
        public int Id { get; set; }

        public int BarangDisplayId { get; set; }
        [ForeignKey("BarangDisplayId")]
        public BarangDisplay? BarangDisplay { get; set; }

        public string TipeMutasi { get; set; } = string.Empty; // "CREATE", "UPDATE", "DELETE", "ROLLBACK"
        public int JumlahKonversi { get; set; } // jumlah konversi_dari_gudang
        public int JumlahStokDisplay { get; set; } // hasil konversi ke pcs misalnya
        public int StokGudangSaatItu { get; set; } // tracking kondisi stok gudang saat itu
        public string? NamaDisplaySnapshot { get; set; }
        public DateTime WaktuMutasi { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}