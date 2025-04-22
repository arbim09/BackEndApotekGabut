using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.DTO
{
    public class StokMutasiBarangDisplayDto
    {
        public int BarangDisplayId { get; set; }
        public string TipeMutasi { get; set; }
        public int JumlahKonversi { get; set; }
        public int JumlahStokDisplay { get; set; }
        public int StokGudangSaatItu { get; set; }
        public DateTime WaktuMutasi { get; set; }
    }
}