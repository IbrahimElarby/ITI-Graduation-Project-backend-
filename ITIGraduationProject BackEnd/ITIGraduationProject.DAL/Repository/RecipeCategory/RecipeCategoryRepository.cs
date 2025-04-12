using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RecipeCategoryRepository : GenericRepository<RecipeCategory>, IRecipeCategoryRepository
    {
        public RecipeCategoryRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public async Task<bool> IsAssigned(int recipeid, int categoryid)
        {
            return await context.RecipeCategories
                .AnyAsync(x => x.RecipeID == recipeid && x.CategoryID == categoryid);
        }

        public async Task<RecipeCategory?> GetByCompositeKeyAsync(int recipeId, int categoryId)
        {
            return await context.Set<RecipeCategory>()
                .FirstOrDefaultAsync(x => x.RecipeID == recipeId && x.CategoryID == categoryId);
        }
    }
}
