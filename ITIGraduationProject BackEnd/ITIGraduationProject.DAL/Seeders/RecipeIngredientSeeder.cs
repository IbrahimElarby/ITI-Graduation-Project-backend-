using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class RecipeIngredientSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient
                {
                    RecipeID = 1,
                    IngredientID = 3,
                    Quantity = 100,
                    Unit = "grams"
                },
                new RecipeIngredient
                {
                    RecipeID = 1,
                    IngredientID = 2,
                    Quantity = 50,
                    Unit = "grams"
                },
                new RecipeIngredient
                {
                    RecipeID = 1,
                    IngredientID = 1,
                    Quantity = 200,
                    Unit = "g"
                }
            );
        }
    }
}
