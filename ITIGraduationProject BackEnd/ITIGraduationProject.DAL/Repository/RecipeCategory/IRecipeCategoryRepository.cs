namespace ITIGraduationProject.DAL
{
    public interface IRecipeCategoryRepository : IGenericRepository<RecipeCategory>
    {
        public Task<bool> IsAssigned(int recipeid, int categoryid);
        public Task<RecipeCategory?> GetByCompositeKeyAsync(int recipeid, int categoryId);
    }
}