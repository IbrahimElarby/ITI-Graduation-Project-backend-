namespace ITIGraduationProject.BL
{
    public interface IFavoriteRecipeManger
    {
        public  Task<GeneralResult> AddToFavoritesAsync(FavoriteRecipeCreateDto dto);
        public  Task<GeneralResult<List<FavoriteRecipeDto>>> GetFavoritesForUser(int userId);

        public Task<GeneralResult> RemoveFromFavoritesAsync(int userId, int recipeId);
    }
}