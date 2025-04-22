using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Apotek.Services.Satuan
{
    public class SatuanService : ISatuanService
    {
        private readonly DataContext _context;
        private readonly ILogger<SatuanService> _logger;

        public SatuanService(DataContext context, ILogger<SatuanService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseAPI> Create(SatuanDto request)
        {
            try
            {
                var satuan = new Models.Satuan
                {
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Set<Models.Satuan>().Add(satuan);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Satuan berhasil ditambahkan",
                    Data = satuan
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal membuat satuan");
                throw;
            }
        }

        public async Task<ResponseAPI> Update(SatuanDto request)
        {
            try
            {
                var satuan = await _context.Set<Models.Satuan>().FindAsync(request.Id);
                if (satuan == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Satuan tidak ditemukan"
                    };
                }

                satuan.Name = request.Name;
                satuan.UpdatedAt = DateTime.UtcNow;

                _context.Update(satuan);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Satuan berhasil diupdate",
                    Data = satuan
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal update satuan");
                throw;
            }
        }

        public async Task<ResponseAPI> Delete(int id)
        {
            try
            {
                var satuan = await _context.Set<Models.Satuan>().FindAsync(id);
                if (satuan == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Satuan tidak ditemukan"
                    };
                }

                _context.Remove(satuan);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Satuan berhasil dihapus"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal hapus satuan");
                throw;
            }
        }

        public async Task<ResponseAPI> GetById(int id)
        {
            try
            {
                var satuan = await _context.Set<Models.Satuan>().FindAsync(id);
                if (satuan == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Satuan tidak ditemukan"
                    };
                }

                return new ResponseAPI
                {
                    Status = true,
                    Data = satuan
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal ambil satuan by id");
                throw;
            }
        }

        public async Task<ResponseAPI> GetAll(SearchDto request)
        {
            try
            {
                var query = _context.Set<Models.Satuan>().AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
                }

                var total = await query.CountAsync();
                var result = await query
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
                        Items = result
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal ambil semua satuan");
                throw;
            }
        }
    }
}
