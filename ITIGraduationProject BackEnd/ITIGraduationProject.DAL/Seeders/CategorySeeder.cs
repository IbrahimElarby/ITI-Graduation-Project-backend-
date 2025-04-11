using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class CategorySeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Name = "Appetizer" },
                new Category { CategoryID = 2, Name = "Vegetarian" },
                new Category { CategoryID = 3, Name = "Dessert" },
                new Category { CategoryID = 4, Name = "Salad" },
                new Category { CategoryID = 5, Name = "Soup" },
                new Category { CategoryID = 6, Name = "Italian" }
            );
        }
    }
}
