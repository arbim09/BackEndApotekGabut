using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Data;
using Apotek.Models;

namespace Apotek.Seeder
{
    public class SupplierSeeder
    {
        public static void SeedSupplierSeeder(DataContext context)
        {
            if (!context.Suppliers.Any())
            {
                var now = DateTime.UtcNow;
                var suppliers = new List<Supplier>
                {
                    new() { Name = "PT. Kimia Farma", Alamat = "Jakarta", No_hp = "081234567891", CreatedAt = now },
                    new() { Name = "CV. Mitra Sehat", Alamat = "Bandung", No_hp = "081298765432", CreatedAt = now },
                    new() { Name = "PT. Apotek Abadi", Alamat = "Surabaya", No_hp = "082112233445", CreatedAt = now },
                    new() { Name = "UD. Farma Jaya", Alamat = "Semarang", No_hp = "081334455667", CreatedAt = now },
                    new() { Name = "CV. Obat Sehat", Alamat = "Yogyakarta", No_hp = "082155566677", CreatedAt = now }
                };

                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }
    }
}