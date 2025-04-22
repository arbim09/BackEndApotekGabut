using Apotek.DTO;
using Apotek.Helpers;

namespace Apotek.Services.KategoriBarang
{
    public interface IKategoriBarangService
    {
        Task<ResponseAPI> Create(KategoriBarangDto request);
        Task<ResponseAPI> Update(KategoriBarangDto request);
        Task<ResponseAPI> Delete(int id);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}
