
using ITIGraduationProject.DAL.Repository;
using ITIGraduationProject.DAL.Repository.Ingredient;
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

        public UnitOfWork(
            ApplicationDbContext cookingContext,
            IPostBlogRepository _PostBlogRepository,
            IRecipeRepository _RecipeRepository,
            ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository
            )
        {
            context = cookingContext;
            PostBlogRepository = _PostBlogRepository;
            RecipeRepository = _RecipeRepository;
            CategoryRepository = categoryRepository;
            IngredientRepository = ingredientRepository;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }


    }
}
