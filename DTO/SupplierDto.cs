using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.DTO
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Alamat { get; set; }
        public string? No_hp { get; set; }
    }
}