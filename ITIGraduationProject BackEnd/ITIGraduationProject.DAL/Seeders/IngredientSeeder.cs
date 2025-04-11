using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class IngredientSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientID = 1,
                    Name = "Tomato",
                    CaloriesPer100g = 18,
                    Carbs = 3.9m,
                    Protein = 0.9m,
                    Fats = 0.2m
                },
                new Ingredient
                {
                    IngredientID = 2,
                    Name = "Cheese",
                    CaloriesPer100g = 402,
                    Carbs = 1.3m,
                    Protein = 25m,
                    Fats = 33m
                },
                new Ingredient
                {
                    IngredientID = 3,
                    Name = "Pasta",
                    CaloriesPer100g = 131,
                    Carbs = 25m,
                    Protein = 5m,
                    Fats = 1.1m
                }
            );
        }
    }
}
