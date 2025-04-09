using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public async Task<Category?> GetByName(string name)
        {
            return await context.Set<Category>()
          .Include(c => c.BlogPosts)
              .ThenInclude(bp => bp.BlogPost)
                  .ThenInclude(p => p.Author)
          .Include(c => c.BlogPosts)
              .ThenInclude(bp => bp.BlogPost)
                  .ThenInclude(p => p.Comments)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Creator)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.RecipeIngredients)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Ratings)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Comments)
          .AsNoTracking()
             .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Name == name);

        }
        public override async Task<Category?> GetByIdAsync(int id)
        {
            return await context.Set<Category>()
          .Include(c => c.BlogPosts)
              .ThenInclude(bp => bp.BlogPost)
                  .ThenInclude(p => p.Author)
          .Include(c => c.BlogPosts)
              .ThenInclude(bp => bp.BlogPost)
                  .ThenInclude(p => p.Comments)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Creator)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.RecipeIngredients)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Ratings)
          .Include(c => c.Recipes)
              .ThenInclude(rc => rc.Recipe)
                  .ThenInclude(r => r.Comments)
          
              .AsSplitQuery() // <-- Improve performance, reduce duplication
            .FirstOrDefaultAsync(c => c.CategoryID == id);
            

        }
        public override async Task<List<Category>> GetAll()
        {
            return await context.Set<Category>()
         .Include(c => c.BlogPosts)
             .ThenInclude(bp => bp.BlogPost)
                 .ThenInclude(p => p.Author)
         .Include(c => c.BlogPosts)
             .ThenInclude(bp => bp.BlogPost)
                 .ThenInclude(p => p.Comments)
         .Include(c => c.Recipes)
             .ThenInclude(rc => rc.Recipe)
                 .ThenInclude(r => r.Creator)
         .Include(c => c.Recipes)
             .ThenInclude(rc => rc.Recipe)
                 .ThenInclude(r => r.RecipeIngredients)
         .Include(c => c.Recipes)
             .ThenInclude(rc => rc.Recipe)
                 .ThenInclude(r => r.Ratings)
         .Include(c => c.Recipes)
             .ThenInclude(rc => rc.Recipe)
                 .ThenInclude(r => r.Comments)
         .AsNoTracking()
         .ToListAsync();


        }
    }
}
