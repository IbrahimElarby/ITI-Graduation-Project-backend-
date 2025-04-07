
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


        
        public IPostBlogRepository PostBlogRepository {  get; }
        public IRecipeRepository RecipeRepository {  get; }
        public UnitOfWork(
            ApplicationDbContext cookingContext ,
            IPostBlogRepository _PostBlogRepository,
            IRecipeRepository _RecipeRepository)
        {
            context = cookingContext;
            PostBlogRepository = _PostBlogRepository;
            RecipeRepository = _RecipeRepository;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        
    }
}
