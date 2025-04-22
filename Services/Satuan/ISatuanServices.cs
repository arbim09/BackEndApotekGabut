using Apotek.DTO;
using Apotek.Helpers;

namespace Apotek.Services.Satuan
{
    public interface ISatuanService
    {
        Task<ResponseAPI> Create(SatuanDto request);
        Task<ResponseAPI> Update(SatuanDto request);
        Task<ResponseAPI> Delete(int id);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}
