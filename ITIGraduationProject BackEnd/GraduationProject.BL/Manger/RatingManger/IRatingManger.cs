using ITIGraduationProject.BL.DTO.RecipeManger.Output;

namespace ITIGraduationProject.BL
{
    public interface IRatingManger
    {
        Task<GeneralResult> AddRatingAsync(RatingDto dto, int userId);
        Task<GeneralResult<List<RatingDto>>> GetRatingsByRecipeAsync(int recipeId);
        Task<GeneralResult<List<RecipeDetailsDTO>>> GetTopRatedRecipesAsync(int count = 9);
    }
}