using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RecipeIngredient
    {
        public int RecipeID { get; set; }
        public Recipe? Recipe { get; set; }
        public int IngredientID { get; set; }
        public Ingredient? Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
