using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Apotek.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _context;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(DataContext context, ILogger<SupplierService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseAPI> Create(SupplierDto request)
        {
            try
            {
                var supplier = new Models.Supplier
                {
                    Name = request.Name,
                    Alamat = request.Alamat,
                    No_hp = request.No_hp,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Supplier berhasil ditambahkan",
                    Data = supplier
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal membuat supplier");
                throw;
            }
        }

        public async Task<ResponseAPI> Update(SupplierDto request)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(request.Id);
                if (supplier == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Supplier tidak ditemukan"
                    };
                }

                supplier.Name = request.Name;
                supplier.Alamat = request.Alamat;
                supplier.No_hp = request.No_hp;
                supplier.UpdatedAt = DateTime.UtcNow;

                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Supplier berhasil diupdate",
                    Data = supplier
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal update supplier ID {Id}", request.Id);
                throw;
            }
        }

        public async Task<ResponseAPI> Delete(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Supplier tidak ditemukan"
                    };
                }

                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "Supplier berhasil dihapus"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal hapus supplier ID {Id}", id);
                throw;
            }
        }

        public async Task<ResponseAPI> GetAll(SearchDto request)
        {
            try
            {
                var query = _context.Suppliers.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Search.ToLower()));
                }

                var totalData = await query.CountAsync();
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
                        TotalData = totalData,
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize,
                        Items = result
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal ambil semua supplier");
                throw;
            }
        }

        public async Task<ResponseAPI> GetById(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Supplier tidak ditemukan"
                    };
                }

                return new ResponseAPI
                {
                    Status = true,
                    Data = supplier
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal ambil supplier ID {Id}", id);
                throw;
            }
        }
    }
}
