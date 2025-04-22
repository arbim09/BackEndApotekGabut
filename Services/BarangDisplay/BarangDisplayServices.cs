using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Apotek.Models;
using Apotek.Services.BarangDisplay;
using Microsoft.EntityFrameworkCore;

public class BarangDisplayService : IBarangDisplayService
{
    private readonly DataContext _context;
    private readonly ILogger<BarangDisplayService> _logger;

    public BarangDisplayService(DataContext context, ILogger<BarangDisplayService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResponseAPI> Create(BarangDisplayDto request)
    {
        try
        {
            var barangGudang = await _context.BarangGudangs.FindAsync(request.BarangGudangId);
            if (barangGudang == null)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Barang gudang tidak ditemukan"
                };
            }

            if (request.Konversi_dari_gudang <= 0)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Konversi dari gudang harus lebih dari 0"
                };
            }
            // Hitung total item display yang akan dibuat (misal: 5 box × 10 tablet = 50 pcs)
            int totalDisplay = RawQuery.HitungStokDisplayFromGudang(barangGudang, request.Konversi_dari_gudang);
            // Hitung stok yang akan dikurangi di gudang
            int stokYangDikurangi = request.Konversi_dari_gudang;

            if (barangGudang.Stok < stokYangDikurangi)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Stok gudang tidak mencukupi"
                };
            }
            barangGudang.Stok -= stokYangDikurangi;
            var display = new BarangDisplay
            {
                BarangGudangId = request.BarangGudangId,
                Nama_display = request.Nama_display,
                Satuan_jual_id = request.Satuan_jual_id,
                Konversi_dari_gudang = request.Konversi_dari_gudang,
                Harga_jual = request.Harga_jual,
                Stok_display = totalDisplay,
                CreatedAt = DateTime.UtcNow
            };

            _context.BarangDisplays.Add(display);
            await _context.SaveChangesAsync();
            _context.StokMutasiBarangDisplays.Add(new StokMutasiBarangDisplay
            {
                BarangDisplayId = display.Id, // setelah SaveChangesAsync baru bisa dapat Id
                TipeMutasi = "CREATE",
                JumlahKonversi = request.Konversi_dari_gudang,
                JumlahStokDisplay = totalDisplay,
                StokGudangSaatItu = barangGudang.Stok,
                WaktuMutasi = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            var result = await _context.BarangDisplays
                .Include(x => x.BarangGudang)
                    .ThenInclude(x => x.Kategori)
                .Include(x => x.BarangGudang)
                    .ThenInclude(x => x.SatuanMasuk)
                .Include(x => x.SatuanJual)
                .FirstOrDefaultAsync(x => x.Id == display.Id);

            return new ResponseAPI
            {
                Status = true,
                Message = "Barang display berhasil ditambahkan",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseAPI
            {
                Status = false,
                StatusCode = 500,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }


    public async Task<ResponseAPI> Update(BarangDisplayDto request)
    {
        try
        {
            var barangDisplay = await _context.BarangDisplays
                .Include(x => x.BarangGudang)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (barangDisplay == null)
            {
                return new ResponseAPI { Status = false, StatusCode = 404, Message = "Barang display tidak ditemukan" };
            }

            var barangGudang = barangDisplay.BarangGudang!;
            if (barangGudang == null || barangGudang.Jumlah_per_satuan <= 0)
            {
                return new ResponseAPI { Status = false, StatusCode = 404, Message = "Barang gudang tidak valid" };
            }
            int stokDikembalikan = barangDisplay.Konversi_dari_gudang;
            barangGudang.Stok += stokDikembalikan;

            // 2. Cek apakah stok cukup untuk konversi baru
            if (barangGudang.Stok < request.Konversi_dari_gudang)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "Stok gudang tidak mencukupi untuk update"
                };
            }

            // 3. Kurangi stok untuk konversi baru
            barangGudang.Stok -= request.Konversi_dari_gudang;

            barangDisplay.Nama_display = request.Nama_display;
            barangDisplay.Harga_jual = request.Harga_jual;
            barangDisplay.Konversi_dari_gudang = request.Konversi_dari_gudang;
            barangDisplay.Stok_display = request.Konversi_dari_gudang * barangGudang.Jumlah_per_satuan;
            barangDisplay.UpdatedAt = DateTime.UtcNow;
            _context.BarangDisplays.Update(barangDisplay);
            _context.StokMutasiBarangDisplays.Add(new StokMutasiBarangDisplay
            {
                BarangDisplayId = barangDisplay.Id,
                TipeMutasi = "UPDATE",
                JumlahKonversi = request.Konversi_dari_gudang,
                JumlahStokDisplay = barangDisplay.Stok_display,
                StokGudangSaatItu = barangGudang.Stok,
                WaktuMutasi = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return new ResponseAPI
            {
                Status = true,
                Message = "Barang display & stok gudang berhasil diupdate",
                Data = new
                {
                    Display = barangDisplay,
                    StokGudangSetelahUpdate = barangGudang.Stok
                }
            };
        }
        catch (Exception ex)
        {
            return new ResponseAPI
            {
                Status = false,
                StatusCode = 500,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }


    public async Task<ResponseAPI> Delete(int id)
    {
        try
        {
            var display = await _context.BarangDisplays
                .Include(x => x.BarangGudang)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (display == null)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Barang display tidak ditemukan"
                };
            }

            var barangGudang = display.BarangGudang;
            if (barangGudang != null && display.Konversi_dari_gudang > 0)
            {
                barangGudang.Stok += display.Konversi_dari_gudang;
            }

            // Tandai soft delete
            display.DeletedAt = DateTime.UtcNow;

            // Catat mutasi
            _context.StokMutasiBarangDisplays.Add(new StokMutasiBarangDisplay
            {
                BarangDisplayId = display.Id,
                TipeMutasi = "DELETE",
                NamaDisplaySnapshot = display.Nama_display,
                JumlahKonversi = display.Konversi_dari_gudang,
                JumlahStokDisplay = display.Stok_display,
                StokGudangSaatItu = barangGudang?.Stok ?? 0,
                WaktuMutasi = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return new ResponseAPI
            {
                Status = true,
                Message = "Barang display berhasil dihapus & stok gudang dikembalikan",
                Data = new
                {
                    stokGudangSetelahHapus = barangGudang?.Stok
                }
            };
        }
        catch (Exception ex)
        {
            return new ResponseAPI
            {
                Status = false,
                StatusCode = 500,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }

    public async Task<ResponseAPI> GetById(int id)
    {
        try
        {
            var barang = await _context.BarangDisplays
                .Include(x => x.BarangGudang)
                .ThenInclude(x => x.Kategori)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (barang == null)
                return new ResponseAPI { Status = false, StatusCode = 404, Message = "Barang tidak ditemukan" };
            return new ResponseAPI { Status = true, Data = barang };
        }
        catch (Exception ex)
        {
            return new ResponseAPI
            {
                Status = false,
                StatusCode = 500,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }

    public async Task<ResponseAPI> GetAll(SearchDto request)
    {
        var query = _context.BarangDisplays
       .Include(x => x.BarangGudang)
           .ThenInclude(x => x.Kategori)
       .Include(x => x.SatuanJual)
       .Where(x => x.DeletedAt == null); // ← filter soft delete

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x => x.Nama_display.ToLower().Contains(request.Search.ToLower()));
        }

        var total = await query.CountAsync();
        var data = await query
            .OrderBy(x => x.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new ResponseAPI
        {
            Status = true,
            Data = new
            {
                TotalData = total,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = data
            }
        };
    }

}

