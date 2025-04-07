using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string? Name { get; set; }
        public decimal CaloriesPer100g { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }

        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
