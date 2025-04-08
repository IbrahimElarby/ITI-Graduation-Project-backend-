namespace ITIGraduationProject.BL.DTO.RecipeManger.Read
{
    //public class RecipeIngredientDTO
    //{
    //    public string Quantity { get; set; } = string.Empty;
    //    public string Unit { get; set; } = string.Empty;
    //    public List<IngredientNameDTO> ingredientNames { get; set; } =new ();
    //}

    //public class RecipeIngredientDto
    //{
    //    public int IngredientID { get; set; }
    //    public string IngredientName { get; set; } // لو أضفت هذه الخاصية في Ingredient
    //    public decimal Quantity { get; set; }
    //    public string Unit { get; set; }
    //}
    public class RecipeIngredientDto
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; } // من كيان Ingredient
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        public decimal CaloriesPer100g { get; set; }
        public decimal Protein { get; set; }
    }
}