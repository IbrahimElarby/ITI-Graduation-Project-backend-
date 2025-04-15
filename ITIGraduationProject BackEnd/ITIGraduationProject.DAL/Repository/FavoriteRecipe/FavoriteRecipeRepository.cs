using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class FavoriteRecipeRepository : IFavoriteRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FavoriteRecipe favorite)
        {
            _context.FavoriteRecipes.Add(favorite);
        
        }

        public async Task RemoveAsync(FavoriteRecipe favorite)
        {
            _context.FavoriteRecipes.Remove(favorite);
            
        }

        public async Task<FavoriteRecipe?> FindAsync(int userId, int recipeId)
        {
            return await _context.FavoriteRecipes
                .FirstOrDefaultAsync(f => f.UserID == userId && f.RecipeID == recipeId);
        }

        public async Task<List<Recipe>> GetFavoritesByUserId(int userId)
        {
            return await _context.FavoriteRecipes
     .Where(f => f.UserID == userId)
     .Include(f => f.Recipe)
         .ThenInclude(r => r.RecipeIngredients)
             .ThenInclude(ri => ri.Ingredient)
     .Include(f => f.Recipe.Ratings)
     .Select(f => f.Recipe)
     .AsSplitQuery()
     .ToListAsync();
        }
    }

}
