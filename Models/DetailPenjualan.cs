using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class DetailPenjualan
    {
        public int Id { get; set; }
        public int Penjualan_id { get; set; }
        public int Barang_id { get; set; }
        public int Qty { get; set; }
        public decimal Harga_jual { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}