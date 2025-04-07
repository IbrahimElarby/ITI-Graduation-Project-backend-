using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Repository
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext cookingContext;

        public RecipeRepository(ApplicationDbContext _cookingContext) : base(_cookingContext)
        {
            cookingContext = _cookingContext;
        }

        public async Task<List<Recipe>> GetByCategory(int catId)
        {
            return await cookingContext.Set<Recipe>()
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .Where(r => r.Categories.Any(rc => rc.CategoryID == catId))
                .ToListAsync();
        }

        public override async Task<List<Recipe>> GetAll()
        {
            return await cookingContext.Set<Recipe>()
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
