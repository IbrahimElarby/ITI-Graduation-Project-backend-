namespace ITIGraduationProject.DAL
{
    public interface IFavoriteRecipeRepository
    {
        Task AddAsync(FavoriteRecipe favorite);
        Task RemoveAsync(FavoriteRecipe favorite);
        Task<FavoriteRecipe?> FindAsync(int userId, int recipeId);
        Task<List<Recipe>> GetFavoritesByUserId(int userId);
    }

}