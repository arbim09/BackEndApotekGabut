using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.DTO;
using Apotek.Helpers;
using static Apotek.Helpers.PageResult;

namespace Apotek.Services.User
{
    public interface IUserService
    {
        Task<ResponseAPI> GetMeAsync();
        Task<ResponseAPI> Register(UserDto request);
        Task<ResponseAPI> Login(UserDto request);
        Task<ResponseAPI> RefreshToken();
        Task<ResponseAPI> UpdateUser(UserDto request);
        Task<ResponseAPI> Delete(UserDto request);
        Task<ResponseAPI> GetById(int id);
        Task<ResponseAPI> GetAll(SearchDto request);
    }
}