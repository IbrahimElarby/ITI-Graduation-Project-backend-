using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class RatingSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasData(
                new Rating
                {
                    RatingID = 1,
                    RecipeID = 1,
                    UserID = 1,
                    Score = 5,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
