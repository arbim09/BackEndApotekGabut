using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Models;
using Microsoft.EntityFrameworkCore;

namespace Apotek.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BarangGudang> BarangGudangs { get; set; }
        public DbSet<KategoriBarang> KategoriBarangs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<BarangMasuk> BarangMasuks { get; set; }
        public DbSet<DetailBarangMasuk> DetailBarangMasuks { get; set; }
        public DbSet<Penjualan> Penjualans { get; set; }
        public DbSet<DetailPenjualan> DetailPenjualans { get; set; }
        public DbSet<Satuan> Satuans { get; set; }
        public DbSet<BarangDisplay> BarangDisplays { get; set; }
        public DbSet<StokMutasiBarangDisplay> StokMutasiBarangDisplays { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "CreatedAt"))
                        entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "UpdatedAt"))
                        entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}