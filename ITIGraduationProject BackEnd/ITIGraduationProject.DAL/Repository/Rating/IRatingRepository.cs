namespace ITIGraduationProject.DAL
{
    public interface IRatingRepository
    {
       public Task AddAsync(Rating rating);
        public Task<List<Rating>> GetRatingsByRecipeIdAsync(int recipeId);
        public Task UpdateRecipeAverageRatingAsync(int recipeId);
        public Task<List<Recipe>> GetTopRatedRecipesAsync(int count);
    }
}