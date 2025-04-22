using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Apotek.Models;
using Microsoft.EntityFrameworkCore;

namespace Apotek.Services.KategoriBarang
{
    public class KategoriBarangService : IKategoriBarangService
    {
        private readonly DataContext _context;

        public KategoriBarangService(DataContext context)
        {
            _context = context;
        }

        public async Task<ResponseAPI> Create(KategoriBarangDto request)
        {
            try
            {
                var kategori = new Models.KategoriBarang
                {
                    Nama_kategori = request.Nama_kategori
                };

                _context.KategoriBarangs.Add(kategori);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Kategori berhasil ditambahkan",
                    Data = kategori
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseAPI> Update(KategoriBarangDto request)
        {
            try
            {
                var kategori = await _context.KategoriBarangs.FindAsync(request.Id);
                if (kategori == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Kategori tidak ditemukan"
                    };
                }

                kategori.Nama_kategori = request.Nama_kategori;
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Kategori berhasil diupdate"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseAPI> Delete(int id)
        {
            try
            {
                var kategori = await _context.KategoriBarangs.FindAsync(id);
                if (kategori == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Kategori tidak ditemukan"
                    };
                }

                _context.KategoriBarangs.Remove(kategori);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Kategori berhasil dihapus"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseAPI> GetById(int id)
        {
            try
            {
                var kategori = await _context.KategoriBarangs.FindAsync(id);
                if (kategori == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Kategori tidak ditemukan"
                    };
                }

                return new ResponseAPI
                {
                    Status = true,
                    Data = kategori
                };
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<ResponseAPI> GetAll(SearchDto request)
        {

            try
            {
                var query = _context.KategoriBarangs.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(k => k.Nama_kategori.ToLower().Contains(request.Search.ToLower()));
                }

                var totalData = await query.CountAsync();
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
                        TotalData = totalData,
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize,
                        Items = data
                    }
                };
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
