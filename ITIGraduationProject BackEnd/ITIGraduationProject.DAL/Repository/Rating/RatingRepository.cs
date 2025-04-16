using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Set<Rating>().Add(rating);
        }

        public async Task<List<Rating>> GetRatingsByRecipeIdAsync(int recipeId)
        {
            return await _context.Ratings
                .Where(r => r.RecipeID == recipeId)
                .ToListAsync();
        }

        public async Task UpdateRecipeAverageRatingAsync(int recipeId)
        {
            var ratings = await GetRatingsByRecipeIdAsync(recipeId);
            var recipe = await _context.Recipes.FindAsync(recipeId);

            if (recipe != null && ratings.Any())
            {
                recipe.AvgRating = Math.Round(ratings.Average(r => r.Score), 2);
                
            }
        }

        public async Task<List<Recipe>> GetTopRatedRecipesAsync(int count)
        {
            return await _context.Recipes
                .OrderByDescending(r => r.AvgRating)
                .Take(count)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri=>ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r=>r.Categories)
                .Include(r=>r.Creator)
                .AsSplitQuery()
                .ToListAsync();
        }
    }

}
