using Microsoft.EntityFrameworkCore;
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
            return await cookingContext.Recipes
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
            return await cookingContext.Recipes
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetByTitle(string title)
        {
            return await cookingContext.Recipes
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .Where(r => r.Title.Contains(title))
                .ToListAsync();
        }
    }
}
