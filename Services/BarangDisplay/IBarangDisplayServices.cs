using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.DTO;
using Apotek.Helpers;

namespace Apotek.Services.BarangDisplay
{
    public interface IBarangDisplayService
    {
        Task<ResponseAPI> Create(BarangDisplayDto request);
        Task<ResponseAPI> Update(BarangDisplayDto request);
        Task<ResponseAPI> Delete(int id);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}