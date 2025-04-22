using Apotek.DTO;
using Apotek.Helpers;

namespace Apotek.Services.Supplier
{
    public interface ISupplierService
    {
        Task<ResponseAPI> Create(SupplierDto request);
        Task<ResponseAPI> Update(SupplierDto request);
        Task<ResponseAPI> Delete(int id);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}
