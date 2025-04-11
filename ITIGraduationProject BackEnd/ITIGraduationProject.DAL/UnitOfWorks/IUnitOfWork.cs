

using ITIGraduationProject.DAL.Repository;


namespace ITIGraduationProject.DAL
{
    public interface IUnitOfWork
    {
        public IPostBlogRepository PostBlogRepository { get; }
        public IRecipeRepository RecipeRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IIngredientRepository IngredientRepository { get; }

        Task<int> SaveChangesAsync();
    }
}