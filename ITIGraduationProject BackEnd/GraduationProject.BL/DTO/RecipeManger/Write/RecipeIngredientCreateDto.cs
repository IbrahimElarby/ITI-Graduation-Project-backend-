using System.ComponentModel.DataAnnotations;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Input
{
    public class RecipeIngredientCreateDto
    {
        public int IngredientID { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; } 
    }
}