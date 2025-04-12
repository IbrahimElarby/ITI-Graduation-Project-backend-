namespace ITIGraduationProject.BL
{
    public interface IRecipeCategoryManger
    {
        public Task<GeneralResult> AssignCategory(int recipeId, int categoryId);

        public Task<GeneralResult> UnAssignCategory(int recipeId, int categoryId);
    }
}