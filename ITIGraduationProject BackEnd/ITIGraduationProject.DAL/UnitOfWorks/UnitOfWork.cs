
using ITIGraduationProject.DAL.Repository;

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


        public UnitOfWork(
            ApplicationDbContext cookingContext,
            IPostBlogRepository _PostBlogRepository,
            IRecipeRepository _RecipeRepository,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository,
            IBlogPostCategoryRepository _blogPostCategoryRepository
            )
        {
            context = cookingContext;
            PostBlogRepository = _PostBlogRepository;
            RecipeRepository = _RecipeRepository;
            CategoryRepository = categoryRepository;
            IngredientRepository = ingredientRepository;
            PostCategoryRepository = _blogPostCategoryRepository;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }


    }
}
