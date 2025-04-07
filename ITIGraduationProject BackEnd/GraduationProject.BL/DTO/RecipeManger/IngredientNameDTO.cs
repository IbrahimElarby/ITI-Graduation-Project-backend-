namespace ITIGraduationProject.BL.DTO.RecipeManger
{
    public class IngredientNameDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal CaloriesPer100g { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
    }
}