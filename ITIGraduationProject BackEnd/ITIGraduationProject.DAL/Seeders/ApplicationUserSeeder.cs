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
            var hasher = new PasswordHasher<ApplicationUser>();

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
                },
                new ApplicationUser
                {
                    Id = 2,
                    UserName = "chef1",
                    NormalizedUserName = "CHEF1",
                    Email = "chef1@example.com",
                    NormalizedEmail = "CHEF1@EXAMPLE.COM",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = 3,
                    UserName = "foodie",
                    NormalizedUserName = "FOODIE",
                    Email = "foodie@example.com",
                    NormalizedEmail = "FOODIE@EXAMPLE.COM",
                    EmailConfirmed = true,
                }
            };

            foreach (var user in users)
            {
                user.PasswordHash = hasher.HashPassword(user, "P@ssword123");
            }

            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }
    }
}
