namespace ITIGraduationProject.BL.DTO.RecipeManger
{
    public class RecipeIngredientDTO
    {
        public string Quantity { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public List<IngredientNameDTO> ingredientNames { get; set; } =new ();
    }
}