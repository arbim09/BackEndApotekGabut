using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Microsoft.EntityFrameworkCore;
using Apotek.Models;

namespace Apotek.Services.BarangGudangService
{
    public class BarangGudangService : IBarangGudangService
    {
        private readonly DataContext _context;
        private readonly ILogger<BarangGudangService> _logger;

        public BarangGudangService(DataContext context, ILogger<BarangGudangService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseAPI> Create(BarangGudangDto request)
        {
            try
            {
                var barang = new BarangGudang
                {
                    Nama_barang = request.Nama_barang,
                    Kategori_id = request.Kategori_id,
                    Satuan_masuk_id = request.Satuan_masuk_id,
                    Harga_beli = request.Harga_beli,
                    Jumlah_per_satuan = request.Jumlah_per_satuan,
                    Stok = request.Stok,
                    CreatedAt = DateTime.UtcNow
                };
                _context.BarangGudangs.Add(barang);
                await _context.SaveChangesAsync();
                var result = await _context.BarangGudangs
                           .OrderBy(x => x.Id)
                           .Include(x => x.Kategori)
                           .Include(x => x.SatuanMasuk)
                           .FirstOrDefaultAsync(x => x.Id == barang.Id);

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Barang gudang berhasil ditambahkan",
                    Data = result
                };
            }
            catch (System.Exception ex)
            {
                return new ResponseAPI
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
                };
                throw;
            }

        }

        public async Task<ResponseAPI> Update(BarangGudangDto request)
        {
            try
            {
                var barang = await _context.BarangGudangs.FindAsync(request.Id);
                if (barang == null)
                    return new ResponseAPI { Status = false, StatusCode = 404, Message = "Barang tidak ditemukan" };

                barang.Nama_barang = request.Nama_barang;
                barang.Kategori_id = request.Kategori_id;
                barang.Satuan_masuk_id = request.Satuan_masuk_id;
                barang.Harga_beli = request.Harga_beli;
                barang.Jumlah_per_satuan = request.Jumlah_per_satuan;
                barang.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                var result = await _context.BarangGudangs
                                         .OrderBy(x => x.Id)
                                         .Include(x => x.Kategori)
                                         .Include(x => x.SatuanMasuk)
                                         .FirstOrDefaultAsync(x => x.Id == barang.Id);

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Barang gudang berhasil ditambahkan",
                    Data = result
                };
                return new ResponseAPI { Status = true, Message = "Barang gudang berhasil diupdate", Data = barang };
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
                var barang = await _context.BarangGudangs.FindAsync(id);
                if (barang == null)
                    return new ResponseAPI { Status = false, StatusCode = 404, Message = "Barang tidak ditemukan" };

                _context.BarangGudangs.Remove(barang);
                await _context.SaveChangesAsync();

                return new ResponseAPI { Status = true, Message = "Barang gudang berhasil dihapus" };
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
                var barang = await _context.BarangGudangs
                    .Include(x => x.Kategori)
                    .Include(x => x.SatuanMasuk)
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
            try
            {
                var query = _context.BarangGudangs
                    .Include(x => x.Kategori)
                    .Include(x => x.SatuanMasuk)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                    query = query.Where(x => x.Nama_barang.ToLower().Contains(request.Search.ToLower()));

                var total = await query.CountAsync();
                var data = await query.OrderBy(x => x.Id)
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
    }
}