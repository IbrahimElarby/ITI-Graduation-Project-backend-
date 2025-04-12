using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class RecipeCategorySeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCategory>().HasData(
                new RecipeCategory
                {
                    RecipeID = 1,
                    CategoryID = 1
                },
                new RecipeCategory
                {
                    RecipeID = 1,
                    CategoryID = 2
                },
                new RecipeCategory
                {
                    RecipeID = 1,
                    CategoryID = 3
                }
            );
        }
    }
}
