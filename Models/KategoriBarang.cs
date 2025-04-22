using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.Models
{
    public class KategoriBarang
    {
        public int Id { get; set; }
        public string Nama_kategori { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}