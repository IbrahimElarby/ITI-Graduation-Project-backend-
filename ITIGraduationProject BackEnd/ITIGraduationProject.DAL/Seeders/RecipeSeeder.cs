using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class RecipeSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    RecipeID = 1,
                    Title = "Spaghetti with Cheese",
                    Description = "A classic Italian dish.",
                    Instructions = "Boil pasta, add cheese.",
                    PrepTime = 10,
                    CookingTime = 15,
                    CuisineType = "Italian",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = 1
                }
        );
        }
    }
}
