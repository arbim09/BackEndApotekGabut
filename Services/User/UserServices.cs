using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Apotek.Data;
using Apotek.DTO;
using Apotek.Helpers;
using Apotek.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Apotek.Services.User
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration, ILogger<UserService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResponseAPI> Register(UserDto request)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
                if (existingUser != null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Username sudah digunakan"
                    };
                }

                var user = new Models.User
                {
                    Name = request.Name,
                    Username = request.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = request.Role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User baru dibuat: {Username}", user.Username);

                return new ResponseAPI
                {
                    Status = true,
                    Message = "User berhasil dibuat",
                    Data = new
                    {
                        user.Id,
                        user.Name,
                        user.Username,
                        user.Role
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal membuat user: {Username}", request.Username);
                throw;
            }
        }


        public async Task<ResponseAPI> UpdateUser(UserDto request)
        {
            try
            {
                var user = await _context.Users.FindAsync(request.Id);
                if (user == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "User tidak ditemukan"
                    };
                }

                user.Name = request.Name;
                user.Role = request.Role;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "User berhasil diupdate",
                    Data = user
                };
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "Terjadi error saat update  user");
                throw; // dilempar ke middleware GlobalErrorHandler
            }

        }

        public async Task<ResponseAPI> Delete(UserDto request)
        {
            try
            {
                var user = await _context.Users.FindAsync(request.Id);
                if (user == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "User tidak ditemukan"
                    };
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new ResponseAPI
                {
                    Status = true,
                    Message = "User berhasil dihapus"
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Terjadi error saat delete USER");
                throw;
            }

        }

        public async Task<ResponseAPI> GetAll(SearchDto request)
        {
            try
            {
                var query = _context.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(u =>
                        u.Name.ToLower().Contains(request.Search.ToLower()) ||
                        u.Username.ToLower().Contains(request.Search.ToLower()));
                }

                var totalData = await query.CountAsync();
                var users = await query
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
                        Items = users.Select(u => new
                        {
                            u.Id,
                            u.Name,
                            u.Username,
                            u.Role
                        })
                    }
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Terjadi error saat Get All USER");
                throw;
            }

        }

        public async Task<ResponseAPI> GetById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "User tidak ditemukan"
                    };
                }

                return new ResponseAPI
                {
                    Status = true,
                    Data = new
                    {
                        user.Id,
                        user.Name,
                        user.Username,
                        user.Role
                    }
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Terjadi error saat Get by id USER");
                throw;
            }

        }

        public async Task<ResponseAPI> GetMeAsync()
        {
            return new ResponseAPI
            {
                Status = true,
                Message = "Fitur belum diimplementasikan"
            };
        }

        public async Task<ResponseAPI> Login(UserDto request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 401,
                        Message = "Username atau password salah"
                    };
                }

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Login berhasil untuk user {Username}", user.Username);

                return new ResponseAPI
                {
                    Status = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    Message = "Login berhasil",
                    Data = new
                    {
                        user.Id,
                        user.Name,
                        user.Username,
                        user.Role
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Terjadi error saat login user: {Username}", request.Username);
                throw; // dilempar ke middleware GlobalErrorHandler
            }
        }

        public async Task<ResponseAPI> RefreshToken()
        {
            try
            {
                // Simulasi: ambil user pertama (nanti bisa diambil dari request/claims)
                var user = await _context.Users.FirstOrDefaultAsync();

                if (user == null || user.RefreshToken == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    _logger.LogWarning("Refresh token tidak valid untuk user ID: {UserId}", user?.Id);
                    return new ResponseAPI
                    {
                        Status = false,
                        StatusCode = 401,
                        Message = "Refresh token tidak valid atau sudah kedaluwarsa"
                    };
                }

                var newToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Refresh token berhasil diperbarui untuk user: {Username}", user.Username);

                return new ResponseAPI
                {
                    Status = true,
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    Message = "Token diperbarui"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Terjadi error saat proses refresh token");
                throw;
            }
        }
        private string GenerateJwtToken(Models.User user)
        {
            try
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(5),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: creds
                );

                _logger.LogInformation("JWT token berhasil digenerate untuk user ID: {UserId}", user.Id);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal generate JWT token untuk user ID: {UserId}", user?.Id);
                throw;
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


    }
}
