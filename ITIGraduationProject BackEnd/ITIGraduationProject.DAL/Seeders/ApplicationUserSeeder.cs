using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ITIGraduationProject.DAL;

namespace ITIGraduationProject.DAL.Seeders
{
    public class ApplicationUserSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "e1a2c65d-d605-4c8a-9915-abcdef123456", // Fixed GUID
                PasswordHash = "AQAAAAIAAYagAAAAEPcu7OacKeTtv8J..." // 👈 Precomputed hash for "P@ssword123"
            },
            new ApplicationUser
            {
                Id = 2,
                UserName = "chef1",
                NormalizedUserName = "CHEF1",
                Email = "chef1@example.com",
                NormalizedEmail = "CHEF1@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "a35cb8d9-3fc4-42b0-8f35-fdfb12345678",
                PasswordHash = "AQAAAAIAAYagAAAAEGzTn+bRzYt5GlXQ..." // 👈 Precomputed hash
            },
            new ApplicationUser
            {
                Id = 3,
                UserName = "foodie",
                NormalizedUserName = "FOODIE",
                Email = "foodie@example.com",
                NormalizedEmail = "FOODIE@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = "c5bfc12a-01cc-4e15-bb65-0123456789ab",
                PasswordHash = "AQAAAAIAAYagAAAAEMtxrU4N3i1xAZlf..." // 👈 Precomputed hash
            }
        };

            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }
    }

}
