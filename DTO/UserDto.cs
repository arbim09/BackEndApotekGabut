using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apotek.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? DiubahOleh { get; set; }
        public string? Keterangan { get; set; }

    }
}