using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apotek.Data;
using Apotek.Models;

namespace Apotek.Seeder
{
    public class UserSeeder
    {
        public static void SeedUser(DataContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Super Admin",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "admin",
                    RefreshToken = null,
                    RefreshTokenExpiryTime = null,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }

    }
}