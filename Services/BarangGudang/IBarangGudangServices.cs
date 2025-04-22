using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.DTO;
using Apotek.Helpers;

namespace Apotek.Services.BarangGudangService
{
    public interface IBarangGudangService
    {
        Task<ResponseAPI> Create(BarangGudangDto request);
        Task<ResponseAPI> Update(BarangGudangDto request);
        Task<ResponseAPI> Delete(int id);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}