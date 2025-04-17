

using ITIGraduationProject.DAL.Repository;
using ITIGraduationProject.DAL.Repository.Account;


namespace ITIGraduationProject.DAL
{
    public interface IUnitOfWork
    {
        public IPostBlogRepository PostBlogRepository { get; }
        public IRecipeRepository RecipeRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IIngredientRepository IngredientRepository { get; }

        public IBlogPostCategoryRepository PostCategoryRepository { get; }

        public IRecipeCategoryRepository RecipeCategoryRepository { get; }

        public IFavoriteRecipeRepository FavoriteRecipeRepository { get; }

        public IRatingRepository RatingRepository { get; }

        public ICommentRepository CommentRepository { get; }
        public IAccountRepository AccountRepository { get; }

        Task<int> SaveChangesAsync();
    }
}