
using ITIGraduationProject.DAL.Repository;
using ITIGraduationProject.DAL.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;



        public IPostBlogRepository PostBlogRepository { get; }
        public IRecipeRepository RecipeRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public IIngredientRepository IngredientRepository { get; }
        public IBlogPostCategoryRepository PostCategoryRepository { get; }

        public IRecipeCategoryRepository RecipeCategoryRepository { get; }

        public IFavoriteRecipeRepository FavoriteRecipeRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public IRatingRepository RatingRepository { get; }

        public ICommentRepository CommentRepository { get; }
        public UnitOfWork(
            ApplicationDbContext cookingContext,
            IPostBlogRepository _PostBlogRepository,
            IRecipeRepository _RecipeRepository,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository,
            IBlogPostCategoryRepository _blogPostCategoryRepository,
            IRecipeCategoryRepository _RecipeCategoryRepository,
            IFavoriteRecipeRepository _FavoriteRecipeRepository,
            IRatingRepository ratingRepository ,
            ICommentRepository commentRepository,
            IAccountRepository accountRepository

            )
        {
            context = cookingContext;
            PostBlogRepository = _PostBlogRepository;
            RecipeRepository = _RecipeRepository;
            CategoryRepository = categoryRepository;
            IngredientRepository = ingredientRepository;
            PostCategoryRepository = _blogPostCategoryRepository;
            RecipeCategoryRepository = _RecipeCategoryRepository;
            FavoriteRecipeRepository = _FavoriteRecipeRepository;
            RatingRepository = ratingRepository;
            CommentRepository = commentRepository;
            AccountRepository = accountRepository;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }


    }
}
