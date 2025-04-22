using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Models;

namespace Apotek.Helpers
{
    public class RawQuery
    {
        public static int HitungStokSetelahPenjualan(BarangGudang barangGudang, BarangDisplay barangDisplay, int qtyDisplayTerjual)
        {
            if (barangDisplay.Konversi_dari_gudang <= 0)
                return barangGudang.Stok; // fallback tidak dikurangi

            int totalQtyDikurangi = barangDisplay.Konversi_dari_gudang * qtyDisplayTerjual;
            return barangGudang.Stok - totalQtyDikurangi;
        }

        public static int HitungStokSetelahBarangMasuk(BarangGudang barangGudang, int qtyMasuk)
        {
            return barangGudang.Stok + (qtyMasuk * barangGudang.Jumlah_per_satuan);
        }

        public static int HitungStokDisplayFromGudang(BarangGudang barangGudang, int jumlahUnitGudang)
        {
            if (jumlahUnitGudang <= 0 || barangGudang == null || barangGudang.Jumlah_per_satuan <= 0)
                return 0;

            return barangGudang.Jumlah_per_satuan * jumlahUnitGudang;
        }

        public static int HitungStokRollback(int konversiLama, int konversiBaru, int isiPerSatuan)
        {
            return (konversiLama - konversiBaru) * isiPerSatuan;
        }



    }
}