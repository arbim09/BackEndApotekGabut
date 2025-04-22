using Apotek.Data;
using Apotek.Models;

namespace Apotek.Seeder
{
    public static class SatuanSeeder
    {
        public static void SeedSatuan(DataContext context)
        {
            if (!context.Set<Satuan>().Any())
            {
                var now = DateTime.UtcNow;
                var satuanList = new List<Satuan>
                {
                    new() { Name = "Pcs", CreatedAt = now },
                    new() { Name = "Box", CreatedAt = now },
                    new() { Name = "Botol", CreatedAt = now },
                    new() { Name = "Tablet", CreatedAt = now },
                    new() { Name = "Strip", CreatedAt = now },
                    new() { Name = "Ampul", CreatedAt = now },
                    new() { Name = "Sachet", CreatedAt = now },
                    new() { Name = "Vial", CreatedAt = now },
                    new() { Name = "Tube", CreatedAt = now },
                    new() { Name = "Blister", CreatedAt = now },
                    new() { Name = "Capsule", CreatedAt = now },
                    new() { Name = "Roll", CreatedAt = now },
                    new() { Name = "Dus", CreatedAt = now },
                    new() { Name = "Pack", CreatedAt = now },
                    new() { Name = "Kotak", CreatedAt = now },
                    new() { Name = "Kaleng", CreatedAt = now },
                    new() { Name = "Lusin", CreatedAt = now },
                    new() { Name = "Jar", CreatedAt = now },
                    new() { Name = "Milliliter", CreatedAt = now },
                    new() { Name = "Liter", CreatedAt = now }
                };

                context.Satuans.AddRange(satuanList);
                context.SaveChanges();
            }
        }
    }
}
