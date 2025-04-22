using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class BarangMasuk
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public int Supplier_id { get; set; }
        public int User_id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}